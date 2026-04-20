import { NextResponse } from 'next/server';
import { cookies } from 'next/headers';
import { readStringField } from '@/lib/api/apiPayload';

function baseUrl() {
  const v = process.env.NEXT_PUBLIC_API_BASE_URL;
  return v && v.length > 0 ? v : 'http://localhost:5043';
}

export async function POST(req: Request) {
  const json = (await req.json().catch(() => null)) as { login?: string; password?: string } | null;
  const login = json?.login ?? '';
  const password = json?.password ?? '';

  if (!login || !password) {
    return NextResponse.json({ error: 'Login/password required' }, { status: 400 });
  }

  const url = `${baseUrl()}/User/login`;
  let res: Response;
  try {
    res = await fetch(url, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ Login: login, Password: password }),
    });
  } catch (e) {
    return NextResponse.json({ error: `Backend error: ${(e as Error).message}` }, { status: 502 });
  }

  const data = await res.json().catch(() => null);
  if (!res.ok) {
    const message = readStringField(data, 'error') || res.statusText || 'Ошибка входа';
    return NextResponse.json({ error: message }, { status: res.status });
  }

  const token = readStringField(data, 'Token', 'token');

  if (!token) {
    return NextResponse.json({ error: 'Токен не получен' }, { status: 500 });
  }

  const store = await cookies();
  store.set('authToken', token, {
    path: '/',
    sameSite: 'lax',
  });

  return NextResponse.json({ token });
}

