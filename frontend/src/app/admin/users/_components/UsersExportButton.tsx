'use client';

import { useSearchParams } from 'next/navigation';
import { Button } from '@/components/ui/Button';

export function UsersExportButton() {
  const sp = useSearchParams();

  function onExport() {
    const qs = sp.toString();
    const href = `/api/admin/users/export${qs ? `?${qs}` : ''}`;
    window.location.href = href;
  }

  return (
    <Button type="button" onClick={onExport}>
      Экспорт Excel (.xlsx)
    </Button>
  );
}

