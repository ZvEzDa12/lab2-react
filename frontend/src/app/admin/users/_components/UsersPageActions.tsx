import Link from 'next/link';
import { UsersExportButton } from '@/app/admin/users/_components/UsersExportButton';

export function UsersPageActions() {
  return (
    <div style={{ display: 'flex', gap: 10, alignItems: 'center', flexWrap: 'wrap' }}>
      <Link className="btn btnPrimary" href="/admin/users/form">
        Создать
      </Link>
      <UsersExportButton />
    </div>
  );
}
