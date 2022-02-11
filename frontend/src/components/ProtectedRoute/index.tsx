import { ReactElement, useContext } from 'react';
import { AppContext } from '@contexts/AppContext';
import { Login } from '@components/Pages/Login';
import { NoAccess } from '@components/Pages/NoAccess';

type ProtectedRouteProps = {
    children: ReactElement
    redirectTo?: string
    requiredRoles?: string[]
}

const ProtectedRoute = ({
    redirectTo = '/',
    children,
    requiredRoles = [],
}: ProtectedRouteProps): JSX.Element => {
    const { user } = useContext(AppContext);

    if (!user || user.id === '') {
        return <Login redirectTo={redirectTo} />;
    }

    if (requiredRoles.length > 0) {
        if (!user.roles.find((r) => requiredRoles.includes(r.normalizedName))) {
            return <NoAccess />;
        }
    }

    return children;
};

export { ProtectedRoute };
