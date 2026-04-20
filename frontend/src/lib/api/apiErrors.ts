import { ApiRequestError } from '@/lib/api/apiClient';

export function isAuthApiError(err: unknown) {
  return err instanceof ApiRequestError && (err.status === 401 || err.status === 403);
}

export function asErrorMessage(err: unknown, fallback = 'Неизвестная ошибка') {
  if (err instanceof Error && err.message) return err.message;
  return fallback;
}
