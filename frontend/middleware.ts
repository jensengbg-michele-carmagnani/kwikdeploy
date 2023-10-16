import { NextResponse } from "next/server"
import type { NextRequest } from "next/server"
import micromatch from "micromatch"
import { getToken } from "next-auth/jwt"

const noAuthRoutes = ["/api/auth/**"]

const noAuthBackendApiRoutes = ["/backendapi/accounts/login"]

export async function middleware(request: NextRequest) {
  const { pathname } = request.nextUrl

  if (micromatch.isMatch(pathname, noAuthRoutes)) {
    return NextResponse.next()
  }

  const token = await getToken({ req: request })

  if (pathname.startsWith("/backendapi")) {
    if (!token && !micromatch.isMatch(pathname, noAuthBackendApiRoutes)) {
      return NextResponse.json(
        { message: "Authentication required" },
        { status: 401 }
      )
    }

    const scheme = process.env.BACKENDAPI_SCHEME!
    const hostname = process.env.BACKENDAPI_HOST!
    const port = process.env.BACKENDAPI_PORT!

    const requestHeaders = new Headers(request.headers)
    requestHeaders.set("host", hostname)
    if (token) {
      requestHeaders.set("Authorization", `Bearer ${token.apiToken}`)
    }
    console.log(requestHeaders)

    let url = request.nextUrl.clone()
    url.protocol = scheme
    url.hostname = hostname
    url.port = port
    url.pathname = url.pathname.replace(/^\/backendapi/, "")

    return NextResponse.rewrite(url, {
      headers: requestHeaders,
    })
  }

  if (!token) {
    const url = new URL("/api/auth/signin", request.url)
    url.searchParams.set("callbackUrl ", encodeURI(request.url))
    return NextResponse.redirect(url)
  }

  return NextResponse.next()
}

export const config = {
  matcher: ["/((?!_next/static|_next/image|favicon.ico).*)"],
}
