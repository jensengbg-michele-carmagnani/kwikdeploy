"use client"

import Link from "next/link"
import { usePathname } from 'next/navigation'
import { cn } from "@/lib/utils"
import { UserNav } from "@/app/components/user-nav"
import { Icons } from "../../components/icons";

const navItems = [
    { name: "Targets", href: "/targets" },
    { name: "Environments", href: "/environments" },
    { name: "App Definitions", href: "/appdefinitions" },
    { name: "Pipelines", href: "/pipelines" },
    { name: "Releases", href: "/releases" },
    { name: "Settings", href: "/settings" },
]

export default function ProjectLevelLayout({
    children,
    params
}: {
    children: React.ReactNode,
    params: { projectId: number }
}) {
    const currentRoute = usePathname()

    return (
        <>
            <div className="border-b">
                <div className="flex h-16 items-center px-4">
                    <nav
                        className={cn("flex items-center space-x-4 lg:space-x-6")}
                    >
                        <Link
                            className="text-sm font-medium transition-colors hover:text-primary"
                            href={`/projects`}
                        >
                            &lt;- Back
                        </Link>
                        {navItems.map((navItem, i) => (
                            <Link key={i}
                                className={
                                    currentRoute.includes(navItem.href) ?
                                        "text-sm font-bold transition-colors hover:text-primary" :
                                        "text-sm font-medium transition-colors hover:text-primary"}
                                href={`/${params.projectId}${navItem.href}`}
                            >
                                {navItem.name}
                            </Link>
                        ))}
                    </nav>
                    <div className="ml-auto flex items-center space-x-4">
                        <UserNav />
                    </div>
                </div>
            </div>
            {children}
        </>
    )
}