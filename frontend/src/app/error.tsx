'use client';

import { useEffect } from 'react';
import { Button } from '@/components/ui/Button';

export default function Error({
  error,
  reset,
}: {
  error: Error & { digest?: string };
  reset: () => void;
}) {
  useEffect(() => {
    console.error(error);
  }, [error]);

  return (
    <div className="card" style={{ marginTop: 16 }}>
      <div className="cardHeader">
        <div style={{ fontWeight: 900, fontSize: 18 }}>Непредвиденная ошибка</div>
      </div>
      <div style={{ color: 'var(--muted)', marginBottom: 14 }}>
        {error.message || 'Что-то пошло не так при загрузке страницы.'}
      </div>
      <Button type="button" variant="primary" onClick={reset}>
        Попробовать снова
      </Button>
    </div>
  );
}
