export function readStringField(
  payload: unknown,
  ...keys: string[]
): string | undefined {
  if (!payload || typeof payload !== 'object') return undefined;
  const obj = payload as Record<string, unknown>;

  for (const key of keys) {
    const value = obj[key];
    if (typeof value === 'string' && value.length > 0) return value;
  }

  return undefined;
}
