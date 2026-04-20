import Link from 'next/link';
import { UserDeleteButton } from '@/app/admin/users/_components/UserDeleteButton';
import { RoleDto, UserDto } from '@/app/admin/users/_lib/usersTable';
import { formatPassport, formatPhone } from '@/lib/phone';

export function UsersTableRows({ users, roles }: { users: UserDto[]; roles: RoleDto[] }) {
  if (users.length === 0) {
    return (
      <tbody>
        <tr>
          <td colSpan={9} style={{ padding: 24, color: 'var(--muted)' }}>
            Нет данных
          </td>
        </tr>
      </tbody>
    );
  }

  return (
    <tbody>
      {users.map((u) => {
        const roleName = u.roleId != null ? roles.find((r) => r.id === u.roleId)?.name : null;
        return (
          <tr key={u.id}>
            <td>{u.id}</td>
            <td>{u.login}</td>
            <td>{u.firstName ?? '-'}</td>
            <td>{u.lastName ?? '-'}</td>
            <td>{u.middleName ?? '-'}</td>
            <td>{u.passportNumber ? formatPassport(u.passportNumber) : '-'}</td>
            <td>{u.phone != null ? formatPhone(u.phone) : '-'}</td>
            <td>{roleName ?? '-'}</td>
            <td>
              <div style={{ display: 'flex', gap: 10, alignItems: 'center', flexWrap: 'wrap' }}>
                <Link className="btn" href={`/admin/users/form?id=${u.id}`}>
                  Редактировать
                </Link>
                <UserDeleteButton userId={u.id} />
              </div>
            </td>
          </tr>
        );
      })}
    </tbody>
  );
}
