import { Icons } from "@/components/icons"
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "@/components/ui/alert-dialog"
import { Button, buttonVariants } from "@/components/ui/button"
import { useRouter } from "next/navigation"
import { Input } from "@/components/ui/input"
import { toast } from "@/components/ui/use-toast"
import useProjects from "@/hooks/useProjects"
import { cn } from "@/lib/utils"
import { zodResolver } from "@hookform/resolvers/zod"

import React, { useCallback, useRef, useState } from "react"
import { useForm } from "react-hook-form"
import z from "zod"
import { setTimeout } from "timers"

interface AlertProps {
  projectName?: string
  id: number
}

export default function AlertComponet({ projectName, id }: AlertProps) {
  const { mutateProjects, isLoading } = useProjects()

  const trimString = (u: unknown) => (typeof u === "string" ? u.trim() : u)

  const [isDisable, setIsDisable] = useState(true)
  const router = useRouter()

  const formSchema = z.object({
    name: z.preprocess(
      trimString,
      z.string().refine((value) => {
        return value === projectName
      })
    ),
  })

  type FormSchemaType = z.infer<typeof formSchema>
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(formSchema),
  })

  const handleOnChangeMatcher = (e: any) => {
    e.preventDefault()

    const input = { name: e.target.value }
    const validation = formSchema.safeParse(input)

    if (validation.success) {
      setIsDisable(false)
    } else setIsDisable(true)
  }

  const handleOnDelete = useCallback(async () => {
    const response = await fetch(`/backendapi/projects/${id}`, {
      method: "DELETE",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ name }),
    })
    if (!response.ok) {
      toast({
        variant: "destructive",
        title: "Uh oh! Something went wrong.",
        description: "There was a problem when saving new project. Try again!",
      })
    } else {
      mutateProjects()
      toast({
        title: "Success!",
        description: "You successfully Delete the project.",
      })
    }
    !isLoading && router.push("/projects")
  }, [])

  return (
    <AlertDialog>
      <AlertDialogTrigger asChild>
        <Button variant="destructive">Delete</Button>
      </AlertDialogTrigger>
      <AlertDialogContent>
        <AlertDialogHeader>
          <AlertDialogTitle>Are you absolutely sure?</AlertDialogTitle>
          <AlertDialogDescription className={cn("leading-6")}>
            This will permanently delete the project {projectName}
            <br />
            To confirm, type
            <span className="font-bold text-black"> {projectName}</span> in the
            box below
          </AlertDialogDescription>
          <Input
            className={cn("mt-16", {
              "focus-visible:ring-red-600": isDisable,
            })}
            onChange={handleOnChangeMatcher}
          />
        </AlertDialogHeader>
        <AlertDialogFooter>
          <AlertDialogCancel>Cancel</AlertDialogCancel>
          <AlertDialogAction
            className={cn(buttonVariants({ variant: "destructive" }))}
            disabled={isDisable}
            onClick={handleOnDelete}
          >
            Cancel
          </AlertDialogAction>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  )
}
