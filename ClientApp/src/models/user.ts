export interface User {
  id: string;
  username: string;
  displayName: string;
  token: string;
}

export interface UserFormValues {
  email: string;
  password: string;
  displayName?: string;
  username?: string;
}
