'use client';

import { useRouter, useSearchParams } from 'next/navigation';
import { useEffect, useMemo, useState } from 'react';
import { Button } from '@/components/ui/Button';
import { Input } from '@/components/ui/Input';
import { Label } from '@/components/ui/Label';
import { Select } from '@/components/ui/Select';

type RoleDto = { id: number; name: string };

function getFirstParam(params: URLSearchParams, key: string) {
  return params.get(key) ?? '';
}

export function UsersFilters({ roles }: { roles: RoleDto[] }) {
  const router = useRouter();
  const sp = useSearchParams();

  const initial = useMemo(() => {
    const q = getFirstParam(sp, 'q');
    const roleId = getFirstParam(sp, 'roleId');
    const hasPhone = getFirstParam(sp, 'hasPhone');
    return { q, roleId, hasPhone };
  }, [sp]);

  const [q, setQ] = useState(initial.q);
  const [roleId, setRoleId] = useState(initial.roleId);
  const [hasPhone, setHasPhone] = useState(initial.hasPhone);

  useEffect(() => {
    setQ(initial.q);
    setRoleId(initial.roleId);
    setHasPhone(initial.hasPhone);
  }, [initial.q, initial.roleId, initial.hasPhone]);

  function pushWithNextParams(next: { q?: string; roleId?: string; hasPhone?: string }) {
    const params = new URLSearchParams(sp.toString());
    if (next.q !== undefined) {
      const trimmed = next.q.trim();
      if (!trimmed) params.delete('q');
      else params.set('q', trimmed);
    }
    if (next.roleId !== undefined) {
      if (!next.roleId) params.delete('roleId');
      else params.set('roleId', next.roleId);
    }
    if (next.hasPhone !== undefined) {
      if (!next.hasPhone) params.delete('hasPhone');
      else params.set('hasPhone', next.hasPhone);
    }

    params.set('page', '1');
    router.push(`/admin/users?${params.toString()}`);
  }

  return (
    <div className="card" style={{ marginBottom: 16 }}>
      <div className="cardHeader">
        <div style={{ fontWeight: 900, fontSize: 18 }}>Фильтры и поиск</div>
        <div style={{ display: 'flex', gap: 10, alignItems: 'center' }}>
          <Button
            type="button"
            variant="primary"
            onClick={() => pushWithNextParams({ q, roleId, hasPhone })}
          >
            Применить
          </Button>
          <Button
            type="button"
            variant="default"
            onClick={() => {
              setQ('');
              setRoleId('');
              setHasPhone('');
              pushWithNextParams({ q: '', roleId: '', hasPhone: '' });
            }}
          >
            Сбросить
          </Button>
        </div>
      </div>

      <div className="grid3">
        <div>
          <Label htmlFor="q">Поиск</Label>
          <Input
            id="q"
            placeholder="login / имя / фамилия / паспорт"
            value={q}
            onChange={(e) => setQ(e.target.value)}
          />
        </div>

        <div>
          <Label htmlFor="roleId">Роль</Label>
          <Select id="roleId" value={roleId} onChange={(e) => setRoleId(e.target.value)}>
            <option value="">Все</option>
            <option value="__null__">Без роли</option>
            {roles.map((r) => (
              <option key={r.id} value={String(r.id)}>
                {r.name}
              </option>
            ))}
          </Select>
        </div>

        <div>
          <Label htmlFor="hasPhone">Телефон</Label>
          <Select id="hasPhone" value={hasPhone} onChange={(e) => setHasPhone(e.target.value)}>
            <option value="">Все</option>
            <option value="yes">Есть</option>
            <option value="no">Нет</option>
          </Select>
        </div>
      </div>
    </div>
  );
}

