import {
    Input as HeroInput,
    InputProps as HeroInputProps,
} from "@heroui/react";

export type InputProps = HeroInputProps;

/**
 * Input wrapper với default props chuẩn hóa
 * Các chỗ gọi không cần khai báo lại placeholder style
 */
export function Input({ className, ...props }: InputProps) {
    return <HeroInput className={className} {...props} />;
}
