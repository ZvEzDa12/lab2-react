import { cookies } from 'next/headers';

export async function getAuthTokenServer() {
  const store = await cookies();

  return store.get('authToken')?.value ?? null;
}

