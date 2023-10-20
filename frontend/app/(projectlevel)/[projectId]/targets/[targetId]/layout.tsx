"use client"

import { SidebarNav } from "./sidebar-nav"
import { Separator } from "@/components/ui/separator"

import useTarget from "@/hooks/useTarget"
import { useParams } from "next/navigation"

const sidebarNavItems = [
  {
    title: "Overview",
    href: "/overview",
  },
  {
    title: "Containers",
    href: "/containers",
  },
  {
    title: "Settings",
    href: "/settings",
  },
]

interface TargetLayoutProps {
  children: React.ReactNode
}

export default function TargetLayout({ children }: TargetLayoutProps) {
  const { projectId, targetId }: { projectId: string; targetId: string } =
    useParams()
  const { target } = useTarget(projectId, targetId)

  return (
    <>
      <div className="hidden space-y-6 p-10 pb-16 md:block">
        <div className="space-y-0.5">
          <h2 className="text-2xl font-bold tracking-tight">{target?.name}</h2>
        </div>
        <Separator className="my-6" />
        <div className="flex flex-col space-y-8 lg:flex-row lg:space-x-12 lg:space-y-0">
          <aside className="-mx-4 lg:w-1/5">
            <SidebarNav items={sidebarNavItems} />
          </aside>
          <div className="flex-1 lg:max-w-2xl">{children}</div>
        </div>
      </div>
    </>
  )
}
