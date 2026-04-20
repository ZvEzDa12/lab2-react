import { apiFetch } from '@/lib/api/apiClient';
import { getAuthTokenServer } from '@/lib/api/auth.server';
import { LoginPrompt } from '@/components/LoginPrompt';
import { UserForm } from '@/app/admin/users/form/_components/UserForm';
import { asErrorMessage, isAuthApiError } from '@/lib/api/apiErrors';
import { RoleDto, UserDto } from '@/app/admin/users/_lib/usersTable';
import { ReactNode } from 'react';

function getFirst(searchParams: Record<string, string | string[] | undefined>, key: string) {
  const v = searchParams[key];
  if (Array.isArray(v)) return v[0] ?? '';
  return v ?? '';
}

export default async function UserFormPage({
  searchParams,
}: {
  searchParams?: Promise<Record<string, string | string[] | undefined>>;
}) {
  function wrapCentered(content: ReactNode) {
    return (
      <div className="formPageCenter">
        <div className="formPageInner">{content}</div>
      </div>
    );
  }

  const sp = (await searchParams) ?? {};
  const idRaw = getFirst(sp, 'id');

  const token = await getAuthTokenServer();
  if (!token) {
    return wrapCentered(
      <LoginPrompt redirectTo={idRaw ? `/admin/users/form?id=${idRaw}` : '/admin/users/form'} />,
    );
  }

  try {
    const rolesRes = await apiFetch<{ items: RoleDto[] }>('Role', {
      method: 'GET',
      token,
      query: { Count: 1000, Offset: 0 },
    });
    const roles = rolesRes.items ?? [];

    if (!idRaw) {
      return wrapCentered(<UserForm mode="create" roles={roles} />);
    }

    const user = await apiFetch<UserDto>(`User/${idRaw}`, { method: 'GET', token });
    return wrapCentered(<UserForm mode="edit" roles={roles} initialUser={user} />);
  } catch (err) {
    if (isAuthApiError(err)) {
      return wrapCentered(
        <LoginPrompt redirectTo={idRaw ? `/admin/users/form?id=${idRaw}` : '/admin/users/form'} />,
      );
    }
    return wrapCentered(
      <div className="card" style={{ marginTop: 16 }}>
        <div style={{ fontWeight: 900, marginBottom: 8 }}>Ошибка загрузки</div>
        <div style={{ color: 'var(--muted)' }}>{asErrorMessage(err)}</div>
      </div>,
    );
  }
}

