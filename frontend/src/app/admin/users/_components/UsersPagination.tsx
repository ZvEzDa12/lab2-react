import Link from 'next/link';

export function UsersPagination({
  page,
  totalPages,
  withParams,
}: {
  page: number;
  totalPages: number;
  withParams: (updates: Record<string, string | undefined | null>) => string;
}) {
  return (
    <div style={{ marginTop: 16, display: 'flex', gap: 12, alignItems: 'center', flexWrap: 'wrap' }}>
      <Link
        className="btn"
        href={withParams({ page: String(Math.max(1, page - 1)) })}
        aria-disabled={page === 1}
        style={page === 1 ? { opacity: 0.5, pointerEvents: 'none' } : undefined}
      >
        Назад
      </Link>

      <div style={{ display: 'flex', gap: 8, flexWrap: 'wrap' }}>
        {Array.from({ length: totalPages }, (_, i) => i + 1)
          .filter((p) => p === 1 || p === totalPages || Math.abs(p - page) <= 2)
          .map((p, idx, arr) => (
            <span key={p}>
              {idx > 0 && arr[idx - 1] !== p - 1 ? <span style={{ padding: '0 4px', color: 'var(--muted)' }}>...</span> : null}
              <Link
                className="btn"
                href={withParams({ page: String(p) })}
                style={p === page ? { borderColor: 'rgba(167,139,250,0.7)', background: 'rgba(167,139,250,0.12)' } : undefined}
              >
                {p}
              </Link>
            </span>
          ))}
      </div>

      <Link
        className="btn"
        href={withParams({ page: String(Math.min(totalPages, page + 1)) })}
        aria-disabled={page >= totalPages}
        style={page >= totalPages ? { opacity: 0.5, pointerEvents: 'none' } : undefined}
      >
        Вперед
      </Link>

      <div style={{ marginLeft: 'auto', color: 'var(--muted)', fontWeight: 700 }}>
        Страница {page} из {totalPages}
      </div>
    </div>
  );
}
