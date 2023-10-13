import { NextResponse } from "next/server";

type loginPros = {
  email: string;
  password: string;
};

export async function POST(request: Request) {
  const { email, password }: loginPros = await request.json();
  const res = await fetch(`${process.env.BASE_URL}/Accaunts/login`, {
    method: "POST",
    body: JSON.stringify({ userNbame: email, password }),
    headers: {
      "Content-Type": "application/json",
    },
  });

  const user = res.json();
  
  if (!user) return NextResponse.error();

  return NextResponse.json(user);
}
