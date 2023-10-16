import { authOptions } from "@/app/(auth)/api/auth/[...nextauth]/authOptions"
import { getServerSession } from "next-auth/next"

export async function getSession() {
  return await getServerSession(authOptions)
}

export default async function getCurrentUser() {}
