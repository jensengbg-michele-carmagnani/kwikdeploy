"use client"

import Link from "next/link"
import { usePathname } from "next/navigation"
import { cn } from "@/lib/utils"
import { UserNav } from "@/components/user-nav"

const navItems = [
  { name: "Projects", href: "/projects" },
  { name: "Settings", href: "/settings" },
  { name: "Sample CRUD", href: "/samplecrud" },
]

export default function NavOrgLevel() {
  const currentRoute = usePathname()

  return (
    <div className="border-b bg-indigo-600 text-gray-200">
      <div className="flex h-16 items-center px-4">
        <nav className={cn("flex items-center space-x-4 lg:space-x-6")}>
          {navItems.map((navItem, i) => (
            <Link
              key={i}
              className={
                currentRoute === navItem.href
                  ? "rounded-md bg-indigo-700 px-3 py-2 text-sm font-medium text-white"
                  : "rounded-md px-3 py-2 text-sm font-medium text-white hover:bg-indigo-500 hover:bg-opacity-75"
              }
              href={navItem.href}
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
  )
}
