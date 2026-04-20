import * as React from 'react';
import { cn } from '@/lib/utils';

export const Table = React.forwardRef<
  HTMLTableElement,
  React.TableHTMLAttributes<HTMLTableElement>
>(({ className, ...props }, ref) => (
  <div className="relative w-full overflow-auto">
    <table
      ref={ref}
      className={cn('table w-full caption-bottom text-sm [&_tr:last-child_td]:border-0', className)}
      {...props}
    />
  </div>
));

Table.displayName = 'Table';

