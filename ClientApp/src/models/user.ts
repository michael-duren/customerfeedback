export interface User {
  id: string;
  userName: string;
  displayName: string;
  token: string;
  roles: string[];
}

export interface UserFormValues {
  email: string;
  password: string;
  displayName?: string;
  userName?: string;
}
