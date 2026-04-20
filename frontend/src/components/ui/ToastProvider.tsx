'use client';

import { createContext, ReactNode, useCallback, useContext, useMemo, useState } from 'react';

type ToastType = 'success' | 'error' | 'info' | 'warning';

type Toast = {
  id: string;
  type: ToastType;
  title: string;
  message?: string;
  durationMs: number;
};

type ToastContextValue = {
  toast: (t: { type: ToastType; title: string; message?: string; durationMs?: number }) => void;
};

const ToastContext = createContext<ToastContextValue | null>(null);

function uid() {
  return Math.random().toString(16).slice(2) + Date.now().toString(16);
}

export function ToastProvider({ children }: { children: ReactNode }) {
  const [toasts, setToasts] = useState<Toast[]>([]);

  const toast = useCallback((t: { type: ToastType; title: string; message?: string; durationMs?: number }) => {
    const durationMs = t.durationMs ?? 4500;
    const id = uid();
    const next: Toast = { id, durationMs, ...t };
    setToasts((prev) => [next, ...prev].slice(0, 4));

    if (durationMs > 0) {
      window.setTimeout(() => {
        setToasts((prev) => prev.filter((x) => x.id !== id));
      }, durationMs);
    }
  }, []);

  const value = useMemo<ToastContextValue>(() => ({ toast }), [toast]);

  return (
    <ToastContext.Provider value={value}>
      {children}
      {toasts.length > 0 ? (
        <div className="toastWrap" aria-live="polite" aria-relevant="additions">
          {toasts.map((t) => (
            <div key={t.id} className="toast" role="status">
              <div className="toastTitle">
                {t.type === 'success' ? 'Успех' : t.type === 'error' ? 'Ошибка' : 'Инфо'}: {t.title}
              </div>
              {t.message ? <div className="toastMsg">{t.message}</div> : null}
            </div>
          ))}
        </div>
      ) : null}
    </ToastContext.Provider>
  );
}

export function useToast() {
  const ctx = useContext(ToastContext);
  if (!ctx) throw new Error('useToast must be used within <ToastProvider />');
  return ctx.toast;
}

