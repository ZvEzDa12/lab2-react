'use client';

import { useRouter } from 'next/navigation';
import { FormEvent, useState } from 'react';
import { z } from 'zod';
import { Button } from '@/components/ui/Button';
import { Input } from '@/components/ui/Input';
import { Label } from '@/components/ui/Label';
import { useToast } from '@/components/ui/ToastProvider';
import { asErrorMessage } from '@/lib/api/apiErrors';
import { readStringField } from '@/lib/api/apiPayload';

const schema = z.object({
  login: z.string().trim().min(1, 'Логин обязателен'),
  password: z.string().min(1, 'Пароль обязателен'),
});

export function LoginPrompt({ redirectTo }: { redirectTo?: string }) {
  const router = useRouter();
  const [login, setLogin] = useState('');
  const [password, setPassword] = useState('');
  const [submitting, setSubmitting] = useState(false);

  const toast = useToast();

  async function onSubmit(e: FormEvent) {
    e.preventDefault();
    const parsed = schema.safeParse({ login, password });
    if (!parsed.success) {
      toast({ type: 'error', title: 'Проверьте поля', message: parsed.error.issues[0]?.message });
      return;
    }

    setSubmitting(true);
    try {
      const res = await fetch('/api/admin/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ login: parsed.data.login, password: parsed.data.password }),
      });

      const data = await res.json().catch(() => null);
      if (!res.ok) {
        throw new Error(readStringField(data, 'error') || 'Ошибка входа');
      }

      toast({ type: 'success', title: 'Вход выполнен' });
      router.refresh();
      if (redirectTo) router.push(redirectTo);
    } catch (err) {
      toast({ type: 'error', title: 'Не удалось войти', message: asErrorMessage(err) });
    } finally {
      setSubmitting(false);
    }
  }

  return (
    <div className="card" style={{ maxWidth: 520, marginTop: 16 }}>
      <div className="cardHeader">
        <div style={{ fontWeight: 900, fontSize: 18 }}>Требуется вход администратора</div>
      </div>
      <form onSubmit={onSubmit} style={{ display: 'grid', gap: 12 }}>
        <div>
          <Label htmlFor="login">Логин</Label>
          <Input id="login" value={login} onChange={(e) => setLogin(e.target.value)} autoComplete="username" />
        </div>
        <div>
          <Label htmlFor="password">Пароль</Label>
          <Input
            id="password"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            autoComplete="current-password"
          />
        </div>
        <Button variant="primary" type="submit" disabled={submitting}>
          {submitting ? 'Вход...' : 'Войти'}
        </Button>
      </form>
    </div>
  );
}

