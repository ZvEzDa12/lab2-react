import { Input } from '@/components/ui/Input';
import { Label } from '@/components/ui/Label';

export function UserFormNameFields({
  firstName,
  lastName,
  middleName,
  setFirstName,
  setLastName,
  setMiddleName,
  fieldErrors,
}: {
  firstName: string;
  lastName: string;
  middleName: string;
  setFirstName: (value: string) => void;
  setLastName: (value: string) => void;
  setMiddleName: (value: string) => void;
  fieldErrors: Record<string, string>;
}) {
  return (
    <>
      <div className="grid2">
        <div>
          <Label htmlFor="firstName">Имя</Label>
          <Input id="firstName" value={firstName} onChange={(e) => setFirstName(e.target.value)} />
          {fieldErrors.firstName ? <div className="errorText">{fieldErrors.firstName}</div> : null}
        </div>
        <div>
          <Label htmlFor="lastName">Фамилия</Label>
          <Input id="lastName" value={lastName} onChange={(e) => setLastName(e.target.value)} />
          {fieldErrors.lastName ? <div className="errorText">{fieldErrors.lastName}</div> : null}
        </div>
      </div>

      <div>
        <Label htmlFor="middleName">Отчество</Label>
        <Input id="middleName" value={middleName} onChange={(e) => setMiddleName(e.target.value)} />
        {fieldErrors.middleName ? <div className="errorText">{fieldErrors.middleName}</div> : null}
      </div>
    </>
  );
}
