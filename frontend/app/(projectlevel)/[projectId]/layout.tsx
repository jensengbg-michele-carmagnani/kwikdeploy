"use client"

import NavProjectLevel from "@/components/nav/NavProjectLevel"

export default function ProjectLevelLayout({
  children,
  params,
}: {
  children: React.ReactNode
  params: { projectId: number }
}) {
  return (
    <>
      <NavProjectLevel params={params} />
      {children}
    </>
  )
}
