import { apiFetch } from '@/lib/api/apiClient';
import { RoleDto, UserDto } from '@/app/admin/users/_lib/usersTable';

const backendPageSize = 500;

export async function getAllRoles(token: string) {
  const rolesRes = await apiFetch<{ items: RoleDto[] }>('Role', {
    method: 'GET',
    token,
    query: { Count: 1000, Offset: 0 },
  });
  return rolesRes.items ?? [];
}

export async function getUsersPage(token: string, offset: number, count: number) {
  const usersRes = await apiFetch<{ items: UserDto[] }>('User', {
    method: 'GET',
    token,
    query: { Count: count, Offset: offset },
  });
  return usersRes.items ?? [];
}

export async function getAllUsers(token: string) {
  const all: UserDto[] = [];
  let offset = 0;

  while (true) {
    const chunk = await getUsersPage(token, offset, backendPageSize);
    all.push(...chunk);
    if (chunk.length < backendPageSize) break;
    offset += backendPageSize;
  }

  return all;
}

export async function getUsersCount(token: string) {
  let total = 0;
  let offset = 0;

  while (true) {
    const chunk = await getUsersPage(token, offset, backendPageSize);
    total += chunk.length;
    if (chunk.length < backendPageSize) break;
    offset += backendPageSize;
  }

  return total;
}
