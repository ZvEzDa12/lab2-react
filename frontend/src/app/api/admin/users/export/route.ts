import { NextResponse } from 'next/server';
import { getAuthTokenServer } from '@/lib/api/auth.server';
import { formatPhone, formatPassport } from '@/lib/phone';
import * as XLSX from 'xlsx';
import { compareByField, getUserSortValue } from '@/app/admin/users/_lib/usersTable';
import { getAllRoles, getAllUsers } from '@/app/admin/users/_lib/usersData';

function getFirst(params: URLSearchParams, key: string) {
  return params.get(key) ?? '';
}

export async function GET(req: Request) {
  const url = new URL(req.url);
  const params = url.searchParams;

  const token = await getAuthTokenServer();
  if (!token) {
    return NextResponse.json({ error: 'Unauthorized' }, { status: 401 });
  }

  const q = getFirst(params, 'q').trim().toLowerCase();
  const roleIdRaw = getFirst(params, 'roleId');
  const hasPhoneRaw = getFirst(params, 'hasPhone');
  const sort = (getFirst(params, 'sort') || 'login').trim();
  const dir = ((getFirst(params, 'dir') || 'asc') as 'asc' | 'desc') === 'desc' ? 'desc' : 'asc';

  const roleId =
    roleIdRaw === '__null__' ? null : roleIdRaw ? Number(roleIdRaw) : undefined;
  const hasPhone =
    hasPhoneRaw === 'yes' ? true : hasPhoneRaw === 'no' ? false : undefined;

  const roles = await getAllRoles(token);
  let users = await getAllUsers(token);

  if (q.length > 0) {
    users = users.filter((u) => {
      const haystack = [
        u.login,
        u.firstName,
        u.lastName,
        u.middleName,
        u.passportNumber ? formatPassport(u.passportNumber) : null,
        u.phone ? formatPhone(u.phone) : null,
        roles.find((r) => r.id === u.roleId)?.name ?? null,
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
    switch (sort) {
      case 'id':
      case 'firstName':
      case 'lastName':
      case 'middleName':
      case 'passportNumber':
      case 'phone':
      case 'roleId':
      case 'login':
        return compareByField(getUserSortValue(a, sort), getUserSortValue(b, sort), dir);
      default:
        return compareByField(getUserSortValue(a, 'login'), getUserSortValue(b, 'login'), dir);
    }
  });

  const rows = users.map((u) => ({
    Id: u.id,
    Login: u.login ?? '',
    Role: roles.find((r) => r.id === u.roleId)?.name ?? '',
    FirstName: u.firstName ?? '',
    LastName: u.lastName ?? '',
    MiddleName: u.middleName ?? '',
    PassportNumber: u.passportNumber ? formatPassport(u.passportNumber) : '',
    Phone: u.phone != null ? formatPhone(u.phone) : '',
  }));

  const worksheet = XLSX.utils.json_to_sheet(rows, {
    header: ['Id', 'Login', 'Role', 'FirstName', 'LastName', 'MiddleName', 'PassportNumber', 'Phone'],
  });
  const workbook = XLSX.utils.book_new();
  XLSX.utils.book_append_sheet(workbook, worksheet, 'Users');
  const xlsxBuffer = XLSX.write(workbook, { type: 'buffer', bookType: 'xlsx' });

  return new NextResponse(xlsxBuffer, {
    headers: {
      'Content-Type': 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
      'Content-Disposition': 'attachment; filename="users.xlsx"',
    },
  });
}

