export class ApiResponse<T = any> {
    success: boolean;
    data: T;
    errors: string[];
}