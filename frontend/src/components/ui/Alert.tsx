import { ReactNode } from 'react';
import { cn } from '@/lib/utils';

type AlertType = 'success' | 'error' | 'info';

export function Alert(props: { type: AlertType; title?: string; children?: ReactNode }) {
  const { type, title, children } = props;
  const alertTitle = title ?? (type === 'error' ? 'Ошибка' : type === 'success' ? 'Успех' : 'Инфо');

  return (
    <div
      className={cn(
        'mt-3 rounded-lg border px-4 py-3 text-sm',
        type === 'success' && 'border-primary/40 bg-primary/10 text-primary-foreground',
        type === 'error' && 'border-destructive/60 bg-destructive/10 text-destructive',
        type === 'info' && 'border-border bg-muted/40 text-foreground',
      )}
      role="alert"
    >
      <div className="font-semibold">{alertTitle}</div>
      {children ? <div className="mt-1 text-xs text-muted-foreground">{children}</div> : null}
    </div>
  );
}

