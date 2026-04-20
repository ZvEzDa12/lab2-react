'use client';

import { FormEvent, useMemo, useState } from 'react';
import { z } from 'zod';
import { apiFetch } from '@/lib/api/apiClient';
import { getAuthTokenClient } from '@/lib/api/auth.client';
import { useRouter } from 'next/navigation';
import { Button } from '@/components/ui/Button';
import { useToast } from '@/components/ui/ToastProvider';
import { formatPassport, formatPhone, parsePhone } from '@/lib/phone';
import { userCreateSchema, userUpdateSchema } from '@/schemas/userSchemas';
import { UserFormNameFields } from '@/app/admin/users/form/_components/UserFormNameFields';
import { UserFormDocumentFields } from '@/app/admin/users/form/_components/UserFormDocumentFields';
import { UserFormCredentialsFields } from '@/app/admin/users/form/_components/UserFormCredentialsFields';
import { asErrorMessage, isAuthApiError } from '@/lib/api/apiErrors';
import { RoleDto, UserDto } from '@/app/admin/users/_lib/usersTable';

function digitsOnly(v: string) {
  return String(v ?? '').replace(/\D/g, '');
}

function zodIssuesToFieldErrors(err: z.ZodError) {
  const out: Record<string, string> = {};
  for (const issue of err.issues) {
    const key = String(issue.path[0] ?? 'form');
    if (!out[key]) out[key] = issue.message;
  }
  return out;
}

export function UserForm({
  mode,
  roles,
  initialUser,
}: {
  mode: 'create' | 'edit';
  roles: RoleDto[];
  initialUser?: UserDto;
}) {
  const router = useRouter();
  const toast = useToast();

  const initial = useMemo(() => {
    return {
      id: initialUser?.id ?? 0,
      login: initialUser?.login ?? '',
      roleId: initialUser?.roleId == null ? '' : String(initialUser.roleId),
      firstName: initialUser?.firstName ?? '',
      lastName: initialUser?.lastName ?? '',
      middleName: initialUser?.middleName ?? '',
      passportNumber: initialUser?.passportNumber ? formatPassport(initialUser.passportNumber) : '',
      phone: initialUser?.phone != null ? formatPhone(initialUser.phone) : '',
      password: '',
    };
  }, [initialUser]);

  const [login, setLogin] = useState(initial.login);
  const [roleId, setRoleId] = useState(initial.roleId);
  const [firstName, setFirstName] = useState(initial.firstName);
  const [lastName, setLastName] = useState(initial.lastName);
  const [middleName, setMiddleName] = useState(initial.middleName);
  const [passportNumber, setPassportNumber] = useState(initial.passportNumber);
  const [phone, setPhone] = useState(initial.phone);
  const [password, setPassword] = useState('');

  const [fieldErrors, setFieldErrors] = useState<Record<string, string>>({});
  const [submitting, setSubmitting] = useState(false);

  async function onSubmit(e: FormEvent) {
    e.preventDefault();
    setSubmitting(true);
    setFieldErrors({});

    const token = getAuthTokenClient();
    if (!token) {
      toast({ type: 'error', title: 'Требуется вход администратора' });
      setSubmitting(false);
      router.push('/admin/users');
      return;
    }

    const payload = {
      id: initial.id,
      login: login.trim(),
      password: password.trim() === '' ? null : password.trim(),
      roleId: roleId === '' ? null : Number(roleId),
      firstName: firstName.trim() === '' ? null : firstName.trim(),
      lastName: lastName.trim() === '' ? null : lastName.trim(),
      middleName: middleName.trim() === '' ? null : middleName.trim(),
      passportNumber: passportNumber.trim() === '' ? null : digitsOnly(passportNumber),
      phone: parsePhone(phone) ?? null,
    };

    try {
      if (mode === 'create') {
        const parsed = userCreateSchema.safeParse(payload);
        if (!parsed.success) {
          setFieldErrors(zodIssuesToFieldErrors(parsed.error));
          toast({ type: 'error', title: 'Проверьте поля', message: 'Форма содержит ошибки' });
          return;
        }

        const body = {
          login: parsed.data.login,
          password: parsed.data.password,
          roleId: parsed.data.roleId ?? null,
          firstName: parsed.data.firstName ?? null,
          lastName: parsed.data.lastName ?? null,
          middleName: parsed.data.middleName ?? null,
          passportNumber: parsed.data.passportNumber ?? null,
          phone: parsed.data.phone ?? null,
        };

        await apiFetch<void>('User/add', { method: 'POST', token, body: body });
        toast({ type: 'success', title: 'Пользователь создан' });
      } else {
        const updatePayload = {
          ...payload,
          password: payload.password,
        };
        const parsed = userUpdateSchema.safeParse(updatePayload);
        if (!parsed.success) {
          setFieldErrors(zodIssuesToFieldErrors(parsed.error));
          toast({ type: 'error', title: 'Проверьте поля', message: 'Форма содержит ошибки' });
          return;
        }

        const body = {
          id: parsed.data.id,
          login: parsed.data.login,
          password: parsed.data.password ?? null,
          roleId: parsed.data.roleId ?? null,
          firstName: parsed.data.firstName ?? null,
          lastName: parsed.data.lastName ?? null,
          middleName: parsed.data.middleName ?? null,
          passportNumber: parsed.data.passportNumber ?? null,
          phone: parsed.data.phone ?? null,
        };

        await apiFetch<void>('User/update', { method: 'PATCH', token, body: body });
        toast({ type: 'success', title: 'Данные сохранены' });
      }

      router.push('/admin/users');
    } catch (err) {
      if (isAuthApiError(err)) {
        toast({ type: 'error', title: 'Сессия истекла', message: 'Войдите повторно' });
        router.push('/admin/users');
        return;
      }
      toast({ type: 'error', title: 'Не удалось сохранить', message: asErrorMessage(err) });
    } finally {
      setSubmitting(false);
    }
  }

  return (
    <div className="card">
      <div className="cardHeader">
        <div style={{ fontWeight: 950, fontSize: 18 }}>{mode === 'create' ? 'Создание пользователя' : 'Редактирование пользователя'}</div>
        <Button type="button" variant="default" onClick={() => router.push('/admin/users')} disabled={submitting}>
          Назад
        </Button>
      </div>

      <form onSubmit={onSubmit} style={{ display: 'grid', gap: 12 }}>
        <UserFormCredentialsFields
          mode={mode}
          roles={roles}
          login={login}
          roleId={roleId}
          password={password}
          setLogin={setLogin}
          setRoleId={setRoleId}
          setPassword={setPassword}
          fieldErrors={fieldErrors}
        />

        <UserFormNameFields
          firstName={firstName}
          lastName={lastName}
          middleName={middleName}
          setFirstName={setFirstName}
          setLastName={setLastName}
          setMiddleName={setMiddleName}
          fieldErrors={fieldErrors}
        />

        <UserFormDocumentFields
          passportNumber={passportNumber}
          phone={phone}
          setPassportNumber={setPassportNumber}
          setPhone={setPhone}
          fieldErrors={fieldErrors}
        />

        <div style={{ display: 'flex', gap: 12, alignItems: 'center', flexWrap: 'wrap' }}>
          <Button variant="primary" type="submit" disabled={submitting}>
            {submitting ? 'Сохранение...' : mode === 'create' ? 'Создать' : 'Сохранить'}
          </Button>
        </div>
      </form>
    </div>
  );
}

