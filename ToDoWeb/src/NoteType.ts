export type Note = {
  id: number;
  userID: number;
  categoryID: number;

  title: string;
  content: string;

  updateDate: string;
  createDate: string;

  state: string;
  isPinned: boolean;
};

export type Category = {
  id: number;
  name: string;
  iconName: string;
};

export type User = {
  id: number;
  email: string;
  userName: string;
  password: string;

  firstName: string;
  lastName: string;

  createDate: string;
  updateDate: string;

  isActive: boolean;
  profileImage: string;
  userType: number;
};

export interface Creds {
  userName: string;
  password: string;
}

export interface registerCreds {
  userName: string;
  password: string;
  email: string;
  firstName: string;
  lastName: string;
  isActive: boolean;
}
