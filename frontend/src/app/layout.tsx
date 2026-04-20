import './globals.css';
import Link from 'next/link';
import { ReactNode } from 'react';
import { ToastProvider } from '@/components/ui/ToastProvider';

export const metadata = {
  title: 'Admin Panel',
  description: 'Административная панель',
};

export default function RootLayout({ children }: { children: ReactNode }) {
  return (
    <html lang="ru">
      <body>
        <ToastProvider>
          <div className="appShell">
            <div className="topbar">
              <div className="container topbarInner">
                <div style={{ fontWeight: 900, letterSpacing: '0.01em' }}>Admin Panel</div>
                <nav className="nav" aria-label="Навигация">
                  <Link href="/">Главная</Link>
                  <Link href="/admin/users">Пользователи</Link>
                  <Link href="/admin/users/form">Форма пользователя</Link>
                </nav>
              </div>
            </div>
            <main className="container appMain">{children}</main>
          </div>
        </ToastProvider>
      </body>
    </html>
  );
}

