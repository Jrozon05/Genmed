export default [
    {
        _name: 'CSidebarNav',
        _children: [
            {
                _name: 'CSidebarNavItem',
                  name: 'Dashboard',
                  to: '/dashboard',
                  icon: 'cil-speedometer'
            },
            {
              _name: 'CSidebarNavTitle',
              _children: ['Configuración']
            },
            {
                _name: 'CSidebarNavItem',
                name: 'Usuarios',
                to: '/usuario',
                icon: 'cil-user'
            },
            {
              _name: 'CSidebarNavItem',
              name: 'Posiciones',
              to: '/posicion',
              icon: 'cil-people'
            }
        ]
    }
]
