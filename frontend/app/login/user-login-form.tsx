"use client";

import { cn } from "@/lib/utils";
import { Input } from "../components/ui/input";
import { Icons } from "../components/icons";
import { Button } from "../components/ui/button";
import { useState } from "react";
import { loginSchema } from "@/lib/validations/auth";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { signIn } from "next-auth/react";

import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/app/components/ui/form";
import { z } from "zod";

interface UserLoginFormProps extends React.HTMLAttributes<HTMLDivElement> {}

type LoginFormValues = z.infer<typeof loginSchema>;

const UserLoginForm = ({ className, ...props }: UserLoginFormProps) => {
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const form = useForm<LoginFormValues>({
    resolver: zodResolver(loginSchema),
    mode: "onChange",
  });

  async function onSubmit(data: LoginFormValues) {
    setIsLoading(true);

    // values after validation
    console.log(data);

    const user = await signIn("credentials", {
      email: data.email,
      password: data.password,
      redirect: false,
    });
    console.log(user);

    setTimeout(() => {
      setIsLoading(false);
    }, 3000);
  }
  return (
    <div className={cn("grid gap-6", className)} {...props}>
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-3">
          <FormField
            control={form.control}
            name="email"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Email</FormLabel>
                <FormControl>
                  <Input placeholder="shadcn" {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="password"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Password</FormLabel>
                <FormControl>
                  <Input type="password" placeholder="******" {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <Button disabled={isLoading} type="submit">
            {isLoading && (
              <Icons.spinner className="mr-2 h-4 w-4 animate-spin" />
            )}
            Sign In
          </Button>
        </form>
      </Form>
    </div>
  );
};

export default UserLoginForm;
