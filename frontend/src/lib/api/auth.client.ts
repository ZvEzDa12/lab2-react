export function getAuthTokenClient() {
  if (typeof window === 'undefined') return null;
  const match = document.cookie.match(/(?:^|; )authToken=([^;]+)/);
  return match ? decodeURIComponent(match[1]) : null;
}

