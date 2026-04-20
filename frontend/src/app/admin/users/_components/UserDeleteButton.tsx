'use client';

import { useRouter } from 'next/navigation';
import { apiFetch } from '@/lib/api/apiClient';
import { getAuthTokenClient } from '@/lib/api/auth.client';
import { Button } from '@/components/ui/Button';
import { useToast } from '@/components/ui/ToastProvider';
import { asErrorMessage, isAuthApiError } from '@/lib/api/apiErrors';

export function UserDeleteButton({ userId }: { userId: number }) {
  const router = useRouter();
  const toast = useToast();

  async function onDelete() {
    if (!confirm('Удалить запись?')) return;

    const token = getAuthTokenClient();
    if (!token) {
      toast({ type: 'error', title: 'Требуется вход администратора', message: 'Нет токена authToken' });
      return;
    }

    try {
      await apiFetch<void>(`User/${userId}`, { method: 'DELETE', token });
      toast({ type: 'success', title: 'Удалено' });
      router.refresh();
    } catch (err) {
      if (isAuthApiError(err)) {
        toast({ type: 'error', title: 'Сессия истекла', message: 'Войдите повторно' });
        router.push('/admin/users');
        return;
      }
      toast({ type: 'error', title: 'Не удалось удалить', message: asErrorMessage(err) });
    }
  }

  return (
    <Button variant="danger" onClick={onDelete} type="button">
      Удалить
    </Button>
  );
}

