"use client"

import React, { useState } from "react"
import {
  Dialog,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog"
import { Button } from "@/components/ui/button"
import { Icons } from "@/components/icons"
import { cn } from "@/lib/utils"
import useTargets from "@/hooks/useTargets"
import { useParams, useRouter } from "next/navigation"

export default function DeleteTargetDialog() {
  const { projectId, targetId }: { projectId: string; targetId: string } =
    useParams()

  const router = useRouter()

  const [open, setOpen] = useState(false)
  const [isSaving, setIsSaving] = useState(false)

  const { mutateTargets } = useTargets(projectId)

  const handleDeleteTarget = async () => {
    setIsSaving(true)

    const response = await fetch(
      `/backendapi/targets/${projectId}/${targetId}`,
      {
        method: "DELETE",
        headers: { "Content-Type": "application/json" },
      }
    )
    if (!response.ok) {
      setOpen(false)
    } else {
      mutateTargets()
      setTimeout(() => {
        setOpen(false)
        router.push(`/${projectId}/targets`)
      }, 500)
    }
    setIsSaving(false)
  }

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>
        <Button variant={"destructive"}>Delete</Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>Delete Target</DialogTitle>
        </DialogHeader>
        <div className="grid gap-4 py-4 group-disabled:opacity-50">
          <p>Are you sure you want to delete this target?</p>
        </div>
        <DialogFooter>
          <fieldset disabled={isSaving} className="group">
            <Button
              variant={"secondary"}
              className="mr-2"
              onClick={() => setOpen(false)}
            >
              Cancel
            </Button>
            <Button
              variant={"destructive"}
              className={cn("relative group-disabled:pointer-events-none")}
              disabled={isSaving}
              onClick={() => handleDeleteTarget()}
            >
              <Icons.spinner
                className={cn(
                  "absolute animate-spin text-slate-100 group-enabled:opacity-0"
                )}
              />
              <span className={cn("group-disabled:opacity-0")}>Delete</span>
            </Button>
          </fieldset>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  )
}
