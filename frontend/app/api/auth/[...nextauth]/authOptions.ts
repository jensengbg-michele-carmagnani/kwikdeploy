import { NextAuthOptions } from "next-auth";
import CredentialsProvider from "next-auth/providers/credentials";

export const authOptions: NextAuthOptions = {
  providers: [
    CredentialsProvider({
      name: "credentials",
      credentials: {
        email: { label: "email", type: "text", placeholder: "johan@mail.com" },
        password: { label: "password", type: "password" },
      },
      async authorize(credentials, req) {
        if (!credentials?.email || !credentials?.password) {
          throw new Error("Invalid credentials");
        }
        const { email, password } = credentials;
        console.log(email);

        const url = `http://localhost:3000/api/login`;
        const res = await fetch(url, {
          method: "POST",
          body: JSON.stringify(credentials),
          headers: {
            "Content-Type": "application/json",
          },
        });
        const user = await res.json();
        if (res.ok && user) return user;

        if (!user) throw new Error(" Invalid Credential ");

        return null;
      },
    }),
  ],

  pages: {
    signIn: "/login",
  },

  session: {
    strategy: "jwt",
  },
  // callbacks: {
  //   async jwt({ token, user, account, isNewUser }) {
  //     if (user) {
  //       if (user.token) {
  //         token = { accessToken: user.token };
  //       }
  //     }
  //     return token;
  //   },

  //   // That token store in session
  //   async session({ session, token }) {
  //     // this token return above jwt()
  //     session.accessToken = token.accessToken;
  //     //if you want to add user details info
  //     session.user = { name: "name", email: "email" }; //this user info get via API call or decode token. Anything you want you can add
  //     return session;
  //   },
  // },

  secret: process.env.NEXTAUTH_SECRET,
};
