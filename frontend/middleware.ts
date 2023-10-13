import { NextRequest, NextResponse } from 'next/server'

export async function middleware(request: NextRequest) {
    const scheme = process.env.BACKENDAPI_SCHEME!
    const hostname = process.env.BACKENDAPI_HOST!
    const port = process.env.BACKENDAPI_PORT!

    // Temporary. Should read JWT from session once Login Form is implemeted with next-auth
    const jwt = process.env.JWT_HARDCODED

    const requestHeaders = new Headers(request.headers)
    requestHeaders.set('host', hostname)
    requestHeaders.set('Authorization', `Bearer ${jwt}`)

    let url = request.nextUrl.clone()
    url.protocol = scheme
    url.hostname = hostname
    url.port = port
    url.pathname = url.pathname.replace(/^\/backendapi/, '');

    return NextResponse.rewrite(url, {
        headers: requestHeaders,
    })
}

export const config = {
    matcher: `/backendapi/:path*`
}