import Link from 'next/link';
import { getAuthTokenServer } from '@/lib/api/auth.server';
import { getAllRoles, getUsersCount } from '@/app/admin/users/_lib/usersData';

export default async function HomePage() {
  let usersCount = 0;
  let rolesCount = 0;

  try {
    const token = await getAuthTokenServer();
    if (token) {
      const [users, roles] = await Promise.all([getUsersCount(token), getAllRoles(token)]);
      usersCount = users;
      rolesCount = roles.length;
    }
  } catch {
    usersCount = 0;
    rolesCount = 0;
  }

  return (
    <div className="dashboardHome">
      <h1 className="dashboardHomeTitle">Панель администратора</h1>

      <section className="card dashboardWelcome">
        <div className="dashboardWelcomeTitle">Добро пожаловать!</div>
        <div className="dashboardWelcomeText">
          Это главная страница панели администратора. Здесь вы можете управлять
          различными сущностями системы.
        </div>
      </section>

      <section className="dashboardStatsGrid">
        <article className="card dashboardStatCard">
          <div className="dashboardStatValue">{usersCount}</div>
          <div className="dashboardStatLabel">Пользователей</div>
        </article>
        <article className="card dashboardStatCard">
          <div className="dashboardStatValue">{rolesCount}</div>
          <div className="dashboardStatLabel">Ролей</div>
        </article>
        <article className="card dashboardStatCard">
          <div className="dashboardStatValue">1</div>
          <div className="dashboardStatLabel">Таблица</div>
        </article>
        <article className="card dashboardStatCard">
          <div className="dashboardStatValue">1</div>
          <div className="dashboardStatLabel">Форма</div>
        </article>
      </section>

      <section className="card dashboardQuickAccess">
        <div className="dashboardQuickTitle">Быстрый доступ</div>
        <div className="dashboardQuickButtons">
          <Link className="btn btnPrimary" href="/admin/users">
            Управление пользователями
          </Link>
          <Link className="btn" href="/admin/users/form">
            Создать пользователя
          </Link>
        </div>
      </section>
    </div>
  );
}

