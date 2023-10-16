import { getToken } from "next-auth/jwt"
import { SuccessfulLoginResponse } from "@/lib/api/web-api-client"
import { NextAuthOptions } from "next-auth"
import CredentialsProvider from "next-auth/providers/credentials"

export const authOptions: NextAuthOptions = {
  providers: [
    CredentialsProvider({
      name: "credentials",
      credentials: {
        userName: { label: "User Name", type: "text" },
        password: { label: "Password", type: "password" },
      },
      async authorize(credentials, req) {
        const response = await fetch(
          `${process.env.NEXTAUTH_URL}/backendapi/accounts/login`,
          {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(credentials),
          }
        )

        if (response.ok) {
          const data: SuccessfulLoginResponse = await response.json()

          return {
            id: data.id!,
            userName: data.username,
            email: data.email,
            token: data.token,
          }
        }

        return null
      },
    }),
  ],

  pages: {},

  callbacks: {
    jwt: async ({ token, user }) => {
      token.apiToken = user.token
      return { ...token, ...user }
    },
    async session({ session, token, user }) {
      session.user.token = token
      return session
    },
  },
}
