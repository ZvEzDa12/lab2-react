import { getAuthTokenServer } from '@/lib/api/auth.server';
import { formatPhone, formatPassport } from '@/lib/phone';
import { UsersFilters } from '@/app/admin/users/_components/UsersFilters';
import { UsersPageActions } from '@/app/admin/users/_components/UsersPageActions';
import { UsersTableHeader } from '@/app/admin/users/_components/UsersTableHeader';
import { UsersTableRows } from '@/app/admin/users/_components/UsersTableRows';
import { UsersPagination } from '@/app/admin/users/_components/UsersPagination';
import { LoginPrompt } from '@/components/LoginPrompt';
import { Suspense } from 'react';
import { Table } from '@/components/ui/Table';
import { asErrorMessage, isAuthApiError } from '@/lib/api/apiErrors';
import {
  SortField,
  UserDto,
  compareByField,
  getFirst,
  getUserSortValue,
  isSortField,
  pageSize,
  toUrlSearchParams,
} from '@/app/admin/users/_lib/usersTable';
import { getAllRoles, getAllUsers, getUsersCount, getUsersPage } from '@/app/admin/users/_lib/usersData';

export default async function UsersPage({
  searchParams,
}: {
  searchParams?: Promise<Record<string, string | string[] | undefined>>;
}) {
  const sp = (await searchParams) ?? {};

  const token = await getAuthTokenServer();
  if (!token) {
    return <LoginPrompt redirectTo="/admin/users" />;
  }

  const q = (getFirst(sp, 'q') || '').trim().toLowerCase();
  const roleIdRaw = getFirst(sp, 'roleId');
  const roleId =
    roleIdRaw === '__null__' ? null : roleIdRaw ? Number(roleIdRaw) : undefined;
  const hasPhoneRaw = getFirst(sp, 'hasPhone');
  const hasPhone = hasPhoneRaw === 'yes' ? true : hasPhoneRaw === 'no' ? false : undefined;

  const page = Math.max(1, parseInt(getFirst(sp, 'page') || '1', 10));
  const offset = (page - 1) * pageSize;

  const sortFieldRaw = getFirst(sp, 'sort') || 'login';
  const sortField = isSortField(sortFieldRaw) ? sortFieldRaw : 'login';
  const dir = (getFirst(sp, 'dir') || 'asc') === 'desc' ? 'desc' : 'asc';

  const filtersActive = q.length > 0 || roleIdRaw !== '' || hasPhone !== undefined;

  try {
    const roles = await getAllRoles(token);

    let users: UserDto[] = [];
    let totalCount = 0;

    if (filtersActive) {
      users = await getAllUsers(token);

      if (q.length > 0) {
        users = users.filter((u) => {
          const haystack = [
            u.login,
            u.firstName,
            u.lastName,
            u.middleName,
            u.passportNumber ? formatPassport(u.passportNumber) : null,
            u.phone != null ? formatPhone(u.phone) : null,
            u.roleId != null ? roles.find((r) => r.id === u.roleId)?.name ?? null : null,
          ]
            .filter(Boolean)
            .join(' ')
            .toLowerCase();
          return haystack.includes(q);
        });
      }

      if (roleIdRaw !== '') {
        users = users.filter((u) => (roleIdRaw === '__null__' ? u.roleId == null : u.roleId === roleId));
      }

      if (hasPhone !== undefined) {
        users = users.filter((u) => (hasPhone ? u.phone != null : u.phone == null));
      }

      users.sort((a, b) => {
        return compareByField(getUserSortValue(a, sortField), getUserSortValue(b, sortField), dir);
      });

      totalCount = users.length;
      users = users.slice(offset, offset + pageSize);
    } else {
      const defaultSort = sortField === 'login' && dir === 'asc';

      if (defaultSort) {
        users = await getUsersPage(token, offset, pageSize);
        totalCount = await getUsersCount(token);
      } else {
        users = await getAllUsers(token);

        users.sort((a, b) => {
          return compareByField(getUserSortValue(a, sortField), getUserSortValue(b, sortField), dir);
        });

        totalCount = users.length;
        users = users.slice(offset, offset + pageSize);
      }
    }

    const totalPages = Math.max(1, Math.ceil(totalCount / pageSize));
    const baseParams = toUrlSearchParams(sp);

    function buildHref(next: URLSearchParams) {
      const qs = next.toString();
      return qs ? `/admin/users?${qs}` : '/admin/users';
    }

    function withParams(updates: Record<string, string | undefined | null>) {
      const p = new URLSearchParams(baseParams.toString());
      for (const [k, v] of Object.entries(updates)) {
        if (v === undefined || v === null || v === '') p.delete(k);
        else p.set(k, v);
      }
      return buildHref(p);
    }

    function sortHref(field: SortField) {
      const isActive = sortField === field;
      const nextDir = isActive ? (dir === 'asc' ? 'desc' : 'asc') : 'asc';
      return withParams({ sort: field, dir: nextDir, page: '1' });
    }

    return (
      <div>
        <Suspense fallback={<div className="card">Загрузка...</div>}>
          <UsersFilters roles={roles} />
        </Suspense>

        <div className="card usersCard">
          <div className="cardHeader">
            <div style={{ fontWeight: 950, fontSize: 18 }}>Таблица пользователей</div>
            <UsersPageActions />
          </div>

          <Table>
            <UsersTableHeader sortField={sortField} dir={dir} sortHref={sortHref} />
            <UsersTableRows users={users} roles={roles} />
          </Table>

          <UsersPagination page={page} totalPages={totalPages} withParams={withParams} />
        </div>
      </div>
    );
  } catch (err) {
    if (isAuthApiError(err)) {
      return <LoginPrompt redirectTo="/admin/users" />;
    }
    return (
      <div className="card" style={{ marginTop: 16 }}>
        <div style={{ fontWeight: 900, marginBottom: 8 }}>Ошибка загрузки данных</div>
        <div style={{ color: 'var(--muted)' }}>{asErrorMessage(err)}</div>
      </div>
    );
  }
}

