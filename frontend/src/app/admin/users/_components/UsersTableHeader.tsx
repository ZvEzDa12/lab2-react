import Link from 'next/link';
import { SortField } from '@/app/admin/users/_lib/usersTable';

type HeaderColumn = {
  field: SortField;
  label: string;
};

const headerColumns: HeaderColumn[] = [
  { field: 'id', label: 'ID' },
  { field: 'login', label: 'Логин' },
  { field: 'firstName', label: 'Имя' },
  { field: 'lastName', label: 'Фамилия' },
  { field: 'middleName', label: 'Отчество' },
  { field: 'passportNumber', label: 'Паспорт' },
  { field: 'phone', label: 'Телефон' },
  { field: 'roleId', label: 'Роль' },
];

export function UsersTableHeader({
  sortField,
  dir,
  sortHref,
}: {
  sortField: SortField;
  dir: 'asc' | 'desc';
  sortHref: (field: SortField) => string;
}) {
  return (
    <thead>
      <tr>
        {headerColumns.map((column) => (
          <th key={column.field}>
            <Link href={sortHref(column.field)}>
              {column.label}
              {sortField === column.field ? (dir === 'asc' ? ' ↑' : ' ↓') : ''}
            </Link>
          </th>
        ))}
        <th>Действия</th>
      </tr>
    </thead>
  );
}
