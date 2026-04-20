import { Input } from '@/components/ui/Input';
import { Label } from '@/components/ui/Label';
import { formatPassport, formatPhone, parsePhone } from '@/lib/phone';

export function UserFormDocumentFields({
  passportNumber,
  phone,
  setPassportNumber,
  setPhone,
  fieldErrors,
}: {
  passportNumber: string;
  phone: string;
  setPassportNumber: (value: string) => void;
  setPhone: (value: string) => void;
  fieldErrors: Record<string, string>;
}) {
  return (
    <div className="grid2">
      <div>
        <Label htmlFor="passportNumber">Паспорт</Label>
        <Input
          id="passportNumber"
          value={passportNumber}
          onChange={(e) => setPassportNumber(formatPassport(e.target.value))}
          placeholder="XXXX XXXXXX"
        />
        {fieldErrors.passportNumber ? <div className="errorText">{fieldErrors.passportNumber}</div> : null}
      </div>

      <div>
        <Label htmlFor="phone">Телефон</Label>
        <Input
          id="phone"
          value={phone}
          onChange={(e) => {
            const digits = e.target.value.replace(/\D/g, '');
            if (digits.length === 0) {
              setPhone('');
              return;
            }
            const parsed = parsePhone(e.target.value);
            setPhone(parsed != null ? formatPhone(parsed) : '');
          }}
          placeholder="+7 (___) ___-__-__"
        />
        {fieldErrors.phone ? <div className="errorText">{fieldErrors.phone}</div> : null}
      </div>
    </div>
  );
}
