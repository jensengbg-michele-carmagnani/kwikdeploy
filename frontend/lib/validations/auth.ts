import * as z from "zod";

export const loginSchema = z.object({
  email: z.string(),
  password: z.string().min(6).max(100),
});
