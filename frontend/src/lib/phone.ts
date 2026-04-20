export function formatPhone(value: number | null | undefined) {
  if (!value) return '';
  const digits = String(value).replace(/\D/g, '');
  if (digits.length === 0) return '';

  let formatted = '+7';
  const phone = digits.startsWith('7') || digits.startsWith('8') ? digits.slice(1) : digits;
  if (phone.length > 0) formatted += ' (' + phone.slice(0, 3);
  if (phone.length >= 3) formatted += ')';
  if (phone.length > 3) formatted += ' ' + phone.slice(3, 6);
  if (phone.length > 6) formatted += '-' + phone.slice(6, 8);
  if (phone.length > 8) formatted += '-' + phone.slice(8, 10);
  return formatted;
}

export function parsePhone(input: string) {
  const digits = String(input ?? '').replace(/\D/g, '');
  if (digits.length === 0) return null;

  const phone = digits.startsWith('7') || digits.startsWith('8') ? '7' + digits.slice(1) : '7' + digits;
  const parsed = parseInt(phone, 10);
  if (!Number.isFinite(parsed) || parsed <= 0) return null;
  return parsed;
}

export function formatPassport(input: string) {
  const digits = String(input ?? '').replace(/\D/g, '');
  if (digits.length === 0) return '';
  if (digits.length <= 4) return digits;
  return digits.slice(0, 4) + ' ' + digits.slice(4, 10);
}

