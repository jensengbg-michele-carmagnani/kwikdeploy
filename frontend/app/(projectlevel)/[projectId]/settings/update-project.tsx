"use client"
import * as React from "react"
import { Button } from "@/components/ui/button"
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { toast } from "../../../../components/ui/use-toast"
import { z } from "zod"
import { SubmitHandler, useForm } from "react-hook-form"
import { zodResolver } from "@hookform/resolvers/zod"
import { Icons } from "../../../../components/icons"
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "../../../../components/ui/form"

import useProjects from "@/hooks/useProjects"
import { cn } from "@/lib/utils"

type CardAttributes = {
  projectId: number
  name?: string
}

type CardProps = React.ComponentProps<typeof Card> & CardAttributes

export default function CardWithForm({
  projectId: id,
  name,
  className,
  ...props
}: CardProps) {
  const { mutateProjects } = useProjects()
  const [isSaving, setIsSaving] = React.useState(false)

  const trimString = (u: unknown) => (typeof u === "string" ? u.trim() : u)
  const formSchema = z.object({
    name: z.preprocess(
      trimString,
      z
        .string()
        .min(1, "Project Name is required")
        .max(20)
        .refine(async (value) => {
          const res = await fetch(`/backendapi/projects/uniquename/${value}`)
          return await res.json()
        }, "Another project with this name already exists")
    ),
  })

  type FormSchemaType = z.infer<typeof formSchema>

  const form = useForm<FormSchemaType>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name,
    },
  })
  const onSubmit: SubmitHandler<FormSchemaType> = async (data) => {
    setIsSaving(true)
    console.log(data)
    const response = await fetch(`/backendapi/projects/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
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
        description: "You successfully modify the project.",
      })
    }
    mutateProjects()
    setIsSaving(false)
  }

  return (
    <Card className={cn("w-full", className)} {...props}>
      <CardHeader>
        <CardTitle>Make changes to your project</CardTitle>
      </CardHeader>
      <CardContent>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)}>
            <div className="grid gap-4 py-4">
              <FormField
                control={form.control}
                name="name"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Name</FormLabel>
                    <FormControl>
                      <Input {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <CardFooter>
              <Button
                className={cn("relative")}
                type="submit"
                disabled={isSaving}
              >
                {!isSaving ? (
                  <span className="">Save</span>
                ) : (
                  <Icons.spinner
                    className="absolut mr-2 h-6 w-6 animate-spin   
               text-slate-100 opacity-100 "
                  />
                )}
              </Button>
            </CardFooter>
          </form>
        </Form>
      </CardContent>
    </Card>
  )
}
