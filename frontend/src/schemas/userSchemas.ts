import { z } from 'zod';

const nullableStringMax = (max: number) =>
  z
    .string()
    .max(max, `Длина не должна превышать ${max} символов`)
    .nullable()
    .optional();

const roleIdSchema = z.number().int().nullable().optional();
const phoneSchema = z.number().int().nullable().optional();

export const userCreateSchema = z.object({
  login: z.string().trim().min(1, 'Обязательное поле').max(32, 'Длина не должна превышать 32'),
  password: z.string().min(1, 'Обязательное поле'),
  roleId: roleIdSchema,
  firstName: nullableStringMax(50),
  lastName: nullableStringMax(50),
  middleName: nullableStringMax(50),
  passportNumber: nullableStringMax(20),
  phone: phoneSchema,
});

export const userUpdateSchema = z.object({
  id: z.number().int().positive('Некорректный Id'),
  login: z.string().trim().min(1, 'Обязательное поле').max(32, 'Длина не должна превышать 32'),
  password: z.string().min(0).max(200).nullable().optional(),
  roleId: roleIdSchema,
  firstName: nullableStringMax(50),
  lastName: nullableStringMax(50),
  middleName: nullableStringMax(50),
  passportNumber: nullableStringMax(20),
  phone: phoneSchema,
});

export type UserCreateValues = z.infer<typeof userCreateSchema>;
export type UserUpdateValues = z.infer<typeof userUpdateSchema>;

