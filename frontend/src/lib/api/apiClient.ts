import { readStringField } from '@/lib/api/apiPayload';

export class ApiRequestError extends Error {
  public status: number;
  public details?: unknown;

  constructor(status: number, message: string, details?: unknown) {
    super(message);
    this.name = 'ApiRequestError';
    this.status = status;
    this.details = details;
  }
}

export type ApiFetchOptions = {
  method?: 'GET' | 'POST' | 'PATCH' | 'DELETE';
  token?: string | null;
  query?: Record<string, string | number | boolean | null | undefined>;
  body?: unknown;
  headers?: Record<string, string>;
};

function baseUrl() {
  const v = process.env.NEXT_PUBLIC_API_BASE_URL;
  return v && v.length > 0 ? v : 'http://localhost:5043';
}

function toQueryString(query?: ApiFetchOptions['query']) {
  if (!query) return '';
  const params = new URLSearchParams();
  for (const [k, val] of Object.entries(query)) {
    if (val === null || val === undefined || val === '') continue;
    params.set(k, String(val));
  }
  return params.toString();
}

export async function apiFetch<T>(path: string, options: ApiFetchOptions = {}): Promise<T> {
  const method = options.method ?? 'GET';
  const queryString = toQueryString(options.query);
  const url = `${baseUrl()}/${path.replace(/^\//, '')}${queryString ? `?${queryString}` : ''}`;

  const headers: Record<string, string> = {
    ...(options.body ? { 'Content-Type': 'application/json' } : {}),
    ...(options.headers ?? {}),
  };
  if (options.token) headers.Authorization = `Bearer ${options.token}`;

  try {
    const res = await fetch(url, {
      method,
      headers,
      body: options.body ? JSON.stringify(options.body) : undefined,
      cache: 'no-store',
    });

    const data = await res.json().catch(() => null);
    if (!res.ok) {
      const message = readStringField(data, 'error') || res.statusText;
      throw new ApiRequestError(res.status, message || 'Ошибка API', data);
    }
    return data as T;
  } catch (err) {
    if (err instanceof ApiRequestError) throw err;
    throw new ApiRequestError(0, (err as Error)?.message || 'Сетевая ошибка', err);
  }
}

