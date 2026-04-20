'use client';

import { useEffect } from 'react';
import { Button } from '@/components/ui/Button';

export default function GlobalError({
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
    <html lang="ru">
      <body>
        <div style={{ maxWidth: 900, margin: '40px auto', padding: '0 16px' }}>
          <div className="card">
            <div className="cardHeader">
              <div style={{ fontWeight: 900, fontSize: 18 }}>Критическая ошибка приложения</div>
            </div>
            <div style={{ color: 'var(--muted)', marginBottom: 14 }}>
              {error.message || 'Приложение столкнулось с ошибкой.'}
            </div>
            <Button type="button" variant="primary" onClick={reset}>
              Перезапустить страницу
            </Button>
          </div>
        </div>
      </body>
    </html>
  );
}
