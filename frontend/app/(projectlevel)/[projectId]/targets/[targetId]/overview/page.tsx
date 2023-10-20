"use client"

import useTarget from "@/hooks/useTarget"
import { useParams } from "next/navigation"

export default function TargetOverview() {
  const { projectId, targetId }: { projectId: string; targetId: string } =
    useParams()
  const { target } = useTarget(projectId, targetId)

  return <h2>Overview</h2>
}
