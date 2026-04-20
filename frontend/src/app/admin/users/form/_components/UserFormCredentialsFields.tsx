import { Input } from '@/components/ui/Input';
import { Label } from '@/components/ui/Label';
import { Select } from '@/components/ui/Select';

type RoleDto = { id: number; name: string };

export function UserFormCredentialsFields({
  mode,
  roles,
  login,
  roleId,
  password,
  setLogin,
  setRoleId,
  setPassword,
  fieldErrors,
}: {
  mode: 'create' | 'edit';
  roles: RoleDto[];
  login: string;
  roleId: string;
  password: string;
  setLogin: (value: string) => void;
  setRoleId: (value: string) => void;
  setPassword: (value: string) => void;
  fieldErrors: Record<string, string>;
}) {
  return (
    <>
      <div className="grid2">
        <div>
          <Label htmlFor="login">Логин</Label>
          <Input id="login" value={login} onChange={(e) => setLogin(e.target.value)} />
          {fieldErrors.login ? <div className="errorText">{fieldErrors.login}</div> : null}
        </div>

        <div>
          <Label htmlFor="roleId">Роль</Label>
          <Select id="roleId" value={roleId} onChange={(e) => setRoleId(e.target.value)}>
            <option value="">Без роли</option>
            {roles.map((r) => (
              <option key={r.id} value={String(r.id)}>
                {r.name}
              </option>
            ))}
          </Select>
          {fieldErrors.roleId ? <div className="errorText">{fieldErrors.roleId}</div> : null}
        </div>
      </div>

      <div>
        <Label htmlFor="password">Пароль</Label>
        <Input
          id="password"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          autoComplete={mode === 'create' ? 'new-password' : 'current-password'}
        />
        {fieldErrors.password ? <div className="errorText">{fieldErrors.password}</div> : null}
      </div>
    </>
  );
}
