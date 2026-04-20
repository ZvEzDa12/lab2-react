export type RoleDto = { id: number; name: string };

export type UserDto = {
  id: number;
  login: string;
  roleId: number | null;
  firstName: string | null;
  lastName: string | null;
  middleName: string | null;
  passportNumber: string | null;
  phone: number | null;
};

export const pageSize = 10;

export const sortableFields = [
  'id',
  'login',
  'firstName',
  'lastName',
  'middleName',
  'passportNumber',
  'phone',
  'roleId',
] as const;

export type SortField = (typeof sortableFields)[number];
export type SortableValue = string | number | null;

export function getFirst(searchParams: Record<string, string | string[] | undefined>, key: string) {
  const v = searchParams[key];
  if (Array.isArray(v)) return v[0] ?? '';
  return v ?? '';
}

export function toUrlSearchParams(searchParams: Record<string, string | string[] | undefined>) {
  const params = new URLSearchParams();
  for (const [k, raw] of Object.entries(searchParams)) {
    const v = Array.isArray(raw) ? raw[0] : raw;
    if (v === undefined || v === '') continue;
    params.set(k, String(v));
  }
  return params;
}

export function compareByField(a: unknown, b: unknown, dir: 'asc' | 'desc') {
  const dirMul = dir === 'asc' ? 1 : -1;
  const av = a ?? '';
  const bv = b ?? '';

  const aNum = typeof av === 'number' ? av : Number(av);
  const bNum = typeof bv === 'number' ? bv : Number(bv);

  if (typeof av === 'number' && typeof bv === 'number') {
    return (aNum - bNum) * dirMul;
  }

  if (!Number.isNaN(aNum) && !Number.isNaN(bNum)) return (aNum - bNum) * dirMul;
  return String(av).localeCompare(String(bv), 'ru') * dirMul;
}

export function isSortField(value: string): value is SortField {
  return (sortableFields as readonly string[]).includes(value);
}

export function getUserSortValue(user: UserDto, field: SortField): SortableValue {
  switch (field) {
    case 'id':
      return user.id;
    case 'login':
      return user.login;
    case 'firstName':
      return user.firstName;
    case 'lastName':
      return user.lastName;
    case 'middleName':
      return user.middleName;
    case 'passportNumber':
      return user.passportNumber;
    case 'phone':
      return user.phone;
    case 'roleId':
      return user.roleId;
    default:
      return user.login;
  }
}
