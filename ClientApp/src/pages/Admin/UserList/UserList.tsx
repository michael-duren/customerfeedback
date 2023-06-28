import React, { useEffect, useState } from 'react';
import { User } from '../../../models/user';
import agent from '../../../api/agent';
import { TbDots } from 'react-icons/tb';
import { RxAvatar } from 'react-icons/rx';
import {
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
} from 'reactstrap';

export default function UserList() {
  const [userList, setUserList] = useState<User[]>([]);
  useEffect(() => {
    agent.AccountApi.getAllUsers().then(setUserList);
  }, []);

  return (
    <div className="container">
      <h2 className="mb-lg-4 gap-2 align-items-center d-flex">
        <RxAvatar /> User List
      </h2>
      <table className="table table-striped">
        <thead className="thead-dark">
          <tr>
            <th className="col">#</th>
            <th className="col">User Id</th>
            <th className="col">UserName</th>
            <th className="col">DisplayName</th>
            <th className="col">Roles</th>
            <th className="col"></th>
          </tr>
        </thead>
        <tbody>
          {userList.map((user, i) => {
            return (
              <tr>
                <th scope="row">{i + 1}</th>
                <th>{user.id}</th>
                <th>{user.userName}</th>
                <th>{user.displayName}</th>
                <th>
                  {user.roles.map((role) => (
                    <span>{role}</span>
                  ))}
                </th>
                <th>
                  <UncontrolledDropdown group>
                    <DropdownToggle color="outline-dark" caret>
                      <TbDots size={20} />
                    </DropdownToggle>
                    <DropdownMenu>
                      <DropdownItem>Delete User</DropdownItem>
                    </DropdownMenu>
                  </UncontrolledDropdown>
                </th>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}
