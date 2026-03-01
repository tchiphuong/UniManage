import { useState, useEffect, useCallback, useRef } from 'react';
import { Link, usePathname, useRouter } from '@/i18n/navigation';
import { useTranslations, useLocale } from 'next-intl';
import Cookies from 'js-cookie';
import {
    ChevronDownIcon,
    HomeIcon,
    FolderOpenIcon,
    UsersIcon,
    CubeIcon,
    DocumentTextIcon,
    CurrencyDollarIcon,
    Cog6ToothIcon,
    CheckCircleIcon,
    ChatBubbleLeftRightIcon,
    ChatBubbleBottomCenterTextIcon,
    MagnifyingGlassIcon,
    StarIcon,
    XMarkIcon,
    ClipboardDocumentCheckIcon,
    ArchiveBoxIcon,
    UserGroupIcon,
    ClockIcon,
    BanknotesIcon,
    CalendarDaysIcon,
    BriefcaseIcon,
    AcademicCapIcon,
    ShoppingCartIcon,
    BuildingStorefrontIcon,
    TruckIcon,
    HeartIcon,
    MegaphoneIcon,
    CalculatorIcon,
    WrenchScrewdriverIcon,
    DocumentDuplicateIcon,
    BellIcon,
    ViewColumnsIcon,
    ChartBarIcon,
    ClipboardDocumentListIcon,
    UserCircleIcon,
    QuestionMarkCircleIcon,
    PlusIcon,
    CheckIcon,
    ChartPieIcon,
} from '@heroicons/react/24/outline';
import {
    StarIcon as StarIconSolid,
    HomeIcon as HomeIconSolid,
    FolderOpenIcon as FolderOpenIconSolid,
    UsersIcon as UsersIconSolid,
    CubeIcon as CubeIconSolid,
    DocumentTextIcon as DocumentTextIconSolid,
    CurrencyDollarIcon as CurrencyDollarIconSolid,
    Cog6ToothIcon as Cog6ToothIconSolid,
    CheckCircleIcon as CheckCircleIconSolid,
    ChatBubbleLeftRightIcon as ChatBubbleLeftRightIconSolid,
    ChatBubbleBottomCenterTextIcon as ChatBubbleBottomCenterTextIconSolid,
    ClipboardDocumentCheckIcon as ClipboardDocumentCheckIconSolid,
    ArchiveBoxIcon as ArchiveBoxIconSolid,
    UserGroupIcon as UserGroupIconSolid,
    ClockIcon as ClockIconSolid,
    BanknotesIcon as BanknotesIconSolid,
    CalendarDaysIcon as CalendarDaysIconSolid,
    BriefcaseIcon as BriefcaseIconSolid,
    AcademicCapIcon as AcademicCapIconSolid,
    ShoppingCartIcon as ShoppingCartIconSolid,
    BuildingStorefrontIcon as BuildingStorefrontIconSolid,
    TruckIcon as TruckIconSolid,
    HeartIcon as HeartIconSolid,
    MegaphoneIcon as MegaphoneIconSolid,
    CalculatorIcon as CalculatorIconSolid,
    WrenchScrewdriverIcon as WrenchScrewdriverIconSolid,
    DocumentDuplicateIcon as DocumentDuplicateIconSolid,
    BellIcon as BellIconSolid,
    ViewColumnsIcon as ViewColumnsIconSolid,
    ChartBarIcon as ChartBarIconSolid,
    ClipboardDocumentListIcon as ClipboardDocumentListIconSolid,
    UserCircleIcon as UserCircleIconSolid,
    QuestionMarkCircleIcon as QuestionMarkCircleIconSolid,
    PlusIcon as PlusIconSolid,
    CheckIcon as CheckIconSolid,
    ChartPieIcon as ChartPieIconSolid,
} from '@heroicons/react/24/solid';
import { useSidebar } from '@/contexts/SidebarContext';
import { MenuItem } from '@/types';
import { useMenu } from '@/services/menu.service';

// Icon mapping - outline versions
const iconComponentsOutline: Record<string, React.ComponentType<{ className?: string }>> = {
    'fa-tachometer-alt': HomeIcon,
    'fa-folder-open': FolderOpenIcon,
    'fa-users': UsersIcon,
    'fa-box': CubeIcon,
    'fa-file-alt': DocumentTextIcon,
    'fa-dollar-sign': CurrencyDollarIcon,
    'fa-cogs': Cog6ToothIcon,
    'fa-check-square': CheckCircleIcon,
    'fa-comments': ChatBubbleLeftRightIcon,
    'fa-quote-left': ChatBubbleBottomCenterTextIcon,
    'fa-clipboard-check': ClipboardDocumentCheckIcon,
    'fa-archive': ArchiveBoxIcon,
    'fa-user-group': UserGroupIcon,
    'fa-clock': ClockIcon,
    'fa-banknotes': BanknotesIcon,
    'fa-calendar': CalendarDaysIcon,
    'fa-briefcase': BriefcaseIcon,
    'fa-graduation-cap': AcademicCapIcon,
    'fa-shopping-cart': ShoppingCartIcon,
    'fa-store': BuildingStorefrontIcon,
    'fa-truck': TruckIcon,
    'fa-heart': HeartIcon,
    'fa-megaphone': MegaphoneIcon,
    'fa-calculator': CalculatorIcon,
    'fa-wrench': WrenchScrewdriverIcon,
    'fa-copy': DocumentDuplicateIcon,
    'fa-bell': BellIcon,
    'fa-columns': ViewColumnsIcon,
    'fa-chart-bar': ChartPieIcon,
    'fa-poll': ChartBarIcon,
    'fa-clipboard-list': ClipboardDocumentListIcon,
    'fa-user-circle': UserCircleIcon,
    'fa-question-circle': QuestionMarkCircleIcon,
    'fa-plus': PlusIcon,
    'fa-check': CheckIcon,
    'fa-history': ClockIcon,
    // Add missing mappings
    'fa-comments-alt': ChatBubbleLeftRightIcon,
};

// Icon mapping - solid versions (for active state)
const iconComponentsSolid: Record<string, React.ComponentType<{ className?: string }>> = {
    'fa-tachometer-alt': HomeIconSolid,
    'fa-folder-open': FolderOpenIconSolid,
    'fa-users': UsersIconSolid,
    'fa-box': CubeIconSolid,
    'fa-file-alt': DocumentTextIconSolid,
    'fa-dollar-sign': CurrencyDollarIconSolid,
    'fa-cogs': Cog6ToothIconSolid,
    'fa-check-square': CheckCircleIconSolid,
    'fa-comments': ChatBubbleLeftRightIconSolid,
    'fa-quote-left': ChatBubbleBottomCenterTextIconSolid,
    'fa-clipboard-check': ClipboardDocumentCheckIconSolid,
    'fa-archive': ArchiveBoxIconSolid,
    'fa-user-group': UserGroupIconSolid,
    'fa-clock': ClockIconSolid,
    'fa-banknotes': BanknotesIconSolid,
    'fa-calendar': CalendarDaysIconSolid,
    'fa-briefcase': BriefcaseIconSolid,
    'fa-graduation-cap': AcademicCapIconSolid,
    'fa-shopping-cart': ShoppingCartIconSolid,
    'fa-store': BuildingStorefrontIconSolid,
    'fa-truck': TruckIconSolid,
    'fa-heart': HeartIconSolid,
    'fa-megaphone': MegaphoneIconSolid,
    'fa-calculator': CalculatorIconSolid,
    'fa-wrench': WrenchScrewdriverIconSolid,
    'fa-copy': DocumentDuplicateIconSolid,
    'fa-bell': BellIconSolid,
    'fa-columns': ViewColumnsIconSolid,
    'fa-chart-bar': ChartPieIconSolid,
    'fa-poll': ChartBarIconSolid,
    'fa-clipboard-list': ClipboardDocumentListIconSolid,
    'fa-user-circle': UserCircleIconSolid,
    'fa-question-circle': QuestionMarkCircleIconSolid,
    'fa-plus': PlusIconSolid,
    'fa-check': CheckIconSolid,
    'fa-history': ClockIconSolid,
    // Add missing mappings
    'fa-comments-alt': ChatBubbleLeftRightIconSolid,
};

// Menu data with translation keys - organized by groups
// Menu data is now fetched from API via useMenu hook

// Badge counts (mock data - in real app, fetch from API)
const badgeCounts: Record<string, number> = {
    todos: 5,
    comments: 12,
    projects: 3,
};

interface MenuItemProps {
    item: MenuItem;
    onNavigate: (link: string) => void;
    currentPath: string;
    isFavorite: boolean;
    onToggleFavorite: (id: string) => void;
    badgeCount?: number;
}

function SidebarMenuItem({
    item,
    currentPath,
    isFavorite,
    onToggleFavorite,
    badgeCount,
}: MenuItemProps) {
    const t = useTranslations();
    const [isOpen, setIsOpen] = useState(false);
    const [showTooltip, setShowTooltip] = useState(false);
    const hasChildren = item.children && item.children.length > 0;

    const isActive =
        item.link === currentPath ||
        (hasChildren && item.children?.some((child) => child.link === currentPath));

    useEffect(() => {
        if (isActive && hasChildren) {
            setIsOpen(true);
        }
    }, [currentPath, hasChildren, isActive]);

    // Use solid icon when active, outline when not
    const IconComponent = item.icon
        ? isActive
            ? iconComponentsSolid[item.icon]
            : iconComponentsOutline[item.icon]
        : null;

    const handleExpandToggle = (e: React.MouseEvent) => {
        if (hasChildren) {
            e.preventDefault();
            setIsOpen(!isOpen);
        }
    };

    const commonClasses = `mx-2 flex flex-1 cursor-pointer items-center justify-between rounded-full p-1.5 transition-all duration-200 ${isActive
        ? 'bg-blue-500 text-white shadow-md shadow-blue-500/25'
        : 'text-gray-700 hover:bg-gray-100 dark:text-zinc-300 dark:hover:bg-zinc-700/50'
        }`;
    const content = (
        <>
            <span className="flex flex-1 items-center gap-3 overflow-hidden">
                {IconComponent && (
                    <span
                        className={`shrink-0 rounded-full p-1.5 transition-colors ${isActive
                            ? 'bg-white/20'
                            : 'bg-gray-100 group-hover:bg-blue-100 dark:bg-zinc-700 dark:group-hover:bg-blue-900/30'
                            }`}
                    >
                        <IconComponent
                            className={`h-4 w-4 ${isActive ? '' : 'group-hover:text-blue-500'}`}
                        />
                    </span>
                )}
                <span className="flex-1 truncate text-left text-sm font-medium">
                    {t(item.title)}
                </span>
            </span>

            <span className="flex shrink-0 items-center gap-1">
                {/* Badge */}
                {badgeCount && badgeCount > 0 && (
                    <span className="rounded-full bg-red-500 px-1.5 py-0.5 text-xs font-bold text-white">
                        {badgeCount > 99 ? '99+' : badgeCount}
                    </span>
                )}

                {/* Keyboard Shortcut */}
                {item.shortcut && !hasChildren && (
                    <kbd
                        className={`hidden rounded-md border px-1.5 py-0.5 text-[10px] font-semibold sm:inline ${isActive
                            ? 'border-white/30 bg-white/20 text-white'
                            : 'border-zinc-300 bg-gray-100 text-gray-500 dark:border-zinc-500 dark:bg-zinc-600 dark:text-zinc-300'
                            }`}
                    >
                        ⌘ + {item.shortcut}
                    </kbd>
                )}

                {/* Favorite Star - inside button */}
                {item.id && (
                    <span
                        onClick={(e) => {
                            e.preventDefault();
                            e.stopPropagation();
                            onToggleFavorite(item.id!);
                        }}
                        className="rounded-full p-1 opacity-0 transition-opacity group-hover:opacity-100 hover:bg-white/20"
                    >
                        {isFavorite ? (
                            <StarIconSolid className="h-4 w-4 text-yellow-500" />
                        ) : (
                            <StarIcon
                                className={`h-4 w-4 ${isActive ? 'text-white/70 hover:text-yellow-300' : 'text-gray-400 hover:text-yellow-500'}`}
                            />
                        )}
                    </span>
                )}

                {hasChildren && (
                    <ChevronDownIcon
                        className={`mx-1 h-4 w-4 transition-transform duration-200 ${isOpen ? 'rotate-180' : ''}`}
                    />
                )}
            </span>
        </>
    );

    return (
        <li className="relative">
            <div className="group relative flex items-center">
                {hasChildren ? (
                    <div
                        onClick={handleExpandToggle}
                        role="button"
                        onMouseEnter={() => setShowTooltip(true)}
                        onMouseLeave={() => setShowTooltip(false)}
                        className={commonClasses}
                    >
                        {content}
                    </div>
                ) : (
                    <Link
                        href={item.link || '#'}
                        onMouseEnter={() => setShowTooltip(true)}
                        onMouseLeave={() => setShowTooltip(false)}
                        className={commonClasses}
                    >
                        {content}
                    </Link>
                )}

                {/* Tooltip */}
                {showTooltip && item.description && (
                    <div className="pointer-events-none absolute left-full z-50 ml-2 w-48 rounded-lg bg-gray-900 px-3 py-2 text-xs text-white opacity-0 shadow-lg transition-opacity group-hover:opacity-100 dark:bg-zinc-700">
                        {t(item.description)}
                        <div className="absolute top-1/2 -left-1 h-2 w-2 -translate-y-1/2 rotate-45 bg-gray-900 dark:bg-zinc-700"></div>
                    </div>
                )}
            </div>

            {hasChildren && isOpen && (
                <ul className="mt-2 ml-6 space-y-0.5 border-l-2 border-zinc-200 dark:border-zinc-700">
                    {item.children?.map((subItem, i) => (
                        <li key={i}>
                            <Link
                                href={subItem.link || '#'}
                                className={`mx-2 flex items-center rounded-full p-1.5 pl-9 text-sm transition-all duration-200 ${subItem.link === currentPath
                                    ? 'bg-blue-500 text-white shadow-md shadow-blue-500/25'
                                    : 'text-gray-600 hover:bg-gray-100 hover:text-gray-900 dark:text-zinc-400 dark:hover:bg-zinc-700/50 dark:hover:text-zinc-200'
                                    }`}
                            >
                                <span className="truncate font-medium">{t(subItem.title)}</span>
                            </Link>
                        </li>
                    ))}
                </ul>
            )}
        </li>
    );
}

import { US, VN, JP, CN, KR, TH } from 'country-flag-icons/react/3x2';

const languages = [
    { code: 'en', name: 'English', Flag: US },
    { code: 'vi', name: 'Tiếng Việt', Flag: VN },
    { code: 'ja', name: '日本語', Flag: JP },
    { code: 'zh', name: '中文', Flag: CN },
    { code: 'ko', name: '한국어', Flag: KR },
    { code: 'th', name: 'ไทย', Flag: TH },
];

const FAVORITES_KEY = 'sidebar-favorites';


// Main Sidebar Component
export function Sidebar() {
    const t = useTranslations();
    const { sidebarOpen, closeSidebar } = useSidebar();
    const { menuData, isLoading } = useMenu();
    const router = useRouter();
    const pathname = usePathname();
    const locale = useLocale();

    const [langOpen, setLangOpen] = useState(false);
    const [searchQuery, setSearchQuery] = useState('');
    const [favorites, setFavorites] = useState<string[]>([]);

    const handleNavigate = (path: string) => {
        // Navigation is handled by Link, but we might want to close sidebar on mobile
        if (window.innerWidth < 1024) {
            closeSidebar();
        }
    };

    // Load favorites from localStorage
    useEffect(() => {
        const saved = localStorage.getItem(FAVORITES_KEY);
        if (saved) {
            setFavorites(JSON.parse(saved));
        }
    }, []);

    // Save favorites to localStorage
    const toggleFavorite = (id: string) => {
        setFavorites((prev) => {
            const newFavorites = prev.includes(id) ? prev.filter((f) => f !== id) : [...prev, id];
            localStorage.setItem(FAVORITES_KEY, JSON.stringify(newFavorites));
            return newFavorites;
        });
    };

    // Language dropdown ref and click-outside handler
    const langRef = useRef<HTMLDivElement>(null);
    useEffect(() => {
        const handleClickOutside = (event: MouseEvent) => {
            if (langRef.current && !langRef.current.contains(event.target as Node)) {
                setLangOpen(false);
            }
        };
        if (langOpen) {
            document.addEventListener('mousedown', handleClickOutside);
        }
        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
    }, [langOpen]);

    const currentLang = languages.find((l) => l.code === locale) || languages[0];



    // Filter menu items based on search
    const filteredMenu = menuData.filter((item) => {
        if (!searchQuery) return true;
        const title = t(item.title).toLowerCase();
        const query = searchQuery.toLowerCase();
        return (
            title.includes(query) ||
            item.children?.some((child) => t(child.title).toLowerCase().includes(query))
        );
    });

    // Separate favorites and regular items
    const favoriteItems = filteredMenu.filter((item) => item.id && favorites.includes(item.id));
    const regularItems = filteredMenu.filter((item) => !item.id || !favorites.includes(item.id));

    // Keyboard shortcuts handler
    useEffect(() => {
        const handleKeyDown = (e: KeyboardEvent) => {
            // Check for Ctrl/Cmd + number
            if ((e.ctrlKey || e.metaKey) && /^[0-9]$/.test(e.key)) {
                e.preventDefault();
                const item = menuData.find((m) => m.shortcut === e.key);
                if (item) {
                    if (item.link) {
                        handleNavigate(item.link);
                    } else if (item.children?.[0]?.link) {
                        handleNavigate(item.children[0].link);
                    }
                }
            }
        };

        window.addEventListener('keydown', handleKeyDown);
        return () => window.removeEventListener('keydown', handleKeyDown);
    }, [handleNavigate]);

    return (
        <aside
            className={`sidebar-transition fixed top-16 left-0 z-40 flex h-[calc(100vh-4rem)] flex-col overflow-hidden bg-white shadow-lg transition-all duration-300 ease-in-out lg:relative lg:top-0 lg:m-3 lg:h-[calc(100vh-5.5rem)] lg:rounded-xl lg:border lg:border-zinc-200 dark:bg-zinc-800 dark:lg:border-zinc-600 ${sidebarOpen
                ? 'w-full translate-x-0 opacity-100 lg:w-64'
                : '-translate-x-full opacity-0 lg:w-0 lg:translate-x-0'
                }`}
        >
            {/* Search Box */}
            {/* Search Box */}
            <div className="dark:bg-content2 border-b border-zinc-200 p-3 dark:border-zinc-700">
                <div className="relative">
                    <MagnifyingGlassIcon className="absolute top-1/2 left-3 h-4 w-4 -translate-y-1/2 text-gray-400" />
                    <input
                        type="text"
                        value={searchQuery}
                        onChange={(e) => setSearchQuery(e.target.value)}
                        placeholder={t('common.search') + '...'}
                        className="w-full rounded-full border border-zinc-200 bg-gray-50 py-2 pr-8 pl-9 text-sm transition-colors focus:border-blue-500 focus:bg-white focus:outline-none dark:border-zinc-600 dark:bg-zinc-700 dark:text-white dark:focus:border-blue-500"
                    />
                    {searchQuery && (
                        <button
                            onClick={() => setSearchQuery('')}
                            className="absolute top-1/2 right-2 -translate-y-1/2 rounded-full p-1 hover:bg-gray-200 dark:hover:bg-gray-600"
                        >
                            <XMarkIcon className="h-4 w-4 text-gray-400" />
                        </button>
                    )}
                </div>
            </div>

            <nav className="flex-1 overflow-x-hidden overflow-y-auto py-4">
                {isLoading ? (
                    <div className="space-y-2 p-4">
                        {[...Array(10)].map((_, i) => (
                            <div
                                key={i}
                                className="h-10 w-full animate-pulse rounded-lg bg-gray-200 dark:bg-zinc-700"
                            ></div>
                        ))}
                    </div>
                ) : (
                    <>
                        {/* Favorites Section */}
                        {favoriteItems.length > 0 && (
                            <div className="mb-4">
                                <div className="mb-2 flex items-center gap-2 px-4">
                                    <StarIconSolid className="h-4 w-4 text-yellow-500" />
                                    <span className="text-xs font-semibold tracking-wider text-gray-500 uppercase dark:text-gray-400">
                                        {t('common.favorites')}
                                    </span>
                                </div>
                                <ul className="space-y-1">
                                    {favoriteItems.map((item, index) => (
                                        <SidebarMenuItem
                                            key={`fav-${index}`}
                                            item={item}
                                            onNavigate={() => handleNavigate(item.link || '#')}
                                            currentPath={pathname}
                                            isFavorite={true}
                                            onToggleFavorite={toggleFavorite}
                                            badgeCount={item.id ? badgeCounts[item.id] : undefined}
                                        />
                                    ))}
                                </ul>
                                <div className="mx-4 my-3 border-t border-zinc-200 dark:border-zinc-700"></div>
                            </div>
                        )}

                        {/* Menu Items - Rendered from API tree */}
                        {regularItems.map((item) => {
                            // Root items with children → section with header
                            if (item.children && item.children.length > 0) {
                                return (
                                    <div key={item.id || item.title} className="mb-2">
                                        {/* Section Header */}
                                        <div className="sticky -top-4 z-10 mb-1 border-b border-zinc-100 bg-white px-4 pb-1 dark:border-zinc-700 dark:bg-zinc-800">
                                            <span className="text-[10px] font-bold tracking-widest text-gray-400 uppercase dark:text-gray-500">
                                                {t(item.title)}
                                            </span>
                                        </div>
                                        {/* Section Items */}
                                        <ul className="space-y-0.5">
                                            {item.children.map((child, index) => (
                                                <SidebarMenuItem
                                                    key={`${item.id}-${index}`}
                                                    item={child}
                                                    onNavigate={() => handleNavigate(child.link || '#')}
                                                    currentPath={pathname}
                                                    isFavorite={
                                                        child.id ? favorites.includes(child.id) : false
                                                    }
                                                    onToggleFavorite={toggleFavorite}
                                                    badgeCount={child.id ? badgeCounts[child.id] : undefined}
                                                />
                                            ))}
                                        </ul>
                                    </div>
                                );
                            }

                            // Root items without children → standalone menu item
                            return (
                                <SidebarMenuItem
                                    key={item.id || item.title}
                                    item={item}
                                    onNavigate={() => handleNavigate(item.link || '#')}
                                    currentPath={pathname}
                                    isFavorite={item.id ? favorites.includes(item.id) : false}
                                    onToggleFavorite={toggleFavorite}
                                    badgeCount={item.id ? badgeCounts[item.id] : undefined}
                                />
                            );
                        })}
                    </>
                )}

                {/* No Results */}
                {filteredMenu.length === 0 && (
                    <div className="px-4 py-8 text-center text-sm text-gray-500 dark:text-gray-400">
                        {t('common.noResults')}
                    </div>
                )}
            </nav>

            {/* Compact Footer: Language + Version + Copyright */}
            <div className="border-t border-zinc-200 px-3 py-2 dark:border-zinc-700">
                <div className="relative flex items-center justify-between">
                    {/* Language Selector - Compact */}
                    <div className="relative" ref={langRef}>
                        <button
                            onClick={() => setLangOpen(!langOpen)}
                            className="flex cursor-pointer items-center gap-1.5 rounded-lg px-2 py-1 text-xs transition-all duration-200 hover:bg-gray-100 dark:hover:bg-gray-700/50"
                        >
                            <currentLang.Flag
                                title={currentLang.name}
                                className="h-4 w-5 rounded-sm"
                            />
                            <ChevronDownIcon
                                className={`h-3 w-3 text-gray-400 transition-transform ${langOpen ? 'rotate-180' : ''}`}
                            />
                        </button>
                        {langOpen && (
                            <div className="absolute bottom-full left-0 z-20 mb-2 min-w-32 overflow-hidden rounded-lg border border-zinc-100 bg-white shadow-lg dark:border-zinc-700 dark:bg-zinc-800">
                                {languages
                                    .sort((a, b) => a.name.localeCompare(b.name))
                                    .filter((lang) => lang.code !== locale)
                                    .map((lang) => (
                                        <button
                                            key={lang.code}
                                            onClick={() => {
                                                Cookies.set('locale', lang.code);
                                                router.refresh();
                                                setLangOpen(false);
                                            }}
                                            className="flex w-full items-center gap-2.5 px-3 py-2 text-sm transition-all hover:bg-gray-50 dark:hover:bg-gray-700/50"
                                        >
                                            <lang.Flag
                                                title={lang.name}
                                                className="h-4 w-5 rounded-sm"
                                            />
                                            <span className="text-gray-700 dark:text-gray-300">
                                                {lang.name}
                                            </span>
                                        </button>
                                    ))}
                            </div>
                        )}
                    </div>

                    {/* Version & Copyright */}
                    <div className="text-right text-[10px] text-gray-400 dark:text-gray-500">
                        <span>v1.0.0</span>
                        <span className="mx-1">•</span>
                        <span>© 2024 Phuong Tran</span>
                    </div>
                </div>
            </div>
        </aside>
    );
}
