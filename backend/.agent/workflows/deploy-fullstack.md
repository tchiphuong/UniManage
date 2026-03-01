# Deploy Full Stack Changes

Deploy both backend and frontend changes to production for the UniManage system.

## Steps

### 1. Pre-Deployment Checklist

Verify all items before deployment:

**Code Quality:**

-   [ ] All tests passing
-   [ ] No console errors or warnings
-   [ ] Code reviewed and approved
-   [ ] No hardcoded credentials or secrets

**Backend:**

-   [ ] Database migrations tested
-   [ ] API endpoints tested with Postman
-   [ ] Logging configured correctly
-   [ ] Connection strings for production set

**Frontend:**

-   [ ] Build succeeds without errors
-   [ ] Environment variables configured
-   [ ] API endpoints point to production
-   [ ] No dev/debug code in production build

**Documentation:**

-   [ ] README updated
-   [ ] API documentation updated
-   [ ] Changelog updated

### 2. Backup Current Production

**Database Backup:**

```bash
# SQL Server backup
sqlcmd -S production-server -Q "BACKUP DATABASE UniManage TO DISK='C:\Backups\UniManage_$(date +%Y%m%d_%H%M%S).bak'"
```

**Application Backup:**

```bash
# Backup current production code
ssh production-server
cd /var/www/unimanage
tar -czf ~/backups/unimanage_$(date +%Y%m%d_%H%M%S).tar.gz .
```

### 3. Run Database Migrations

**Test in staging first:**

```bash
# Connect to staging database
sqlcmd -S staging-server -d UniManage -i backend/database/migrations/XX_MigrationName.sql
```

**Apply to production:**

```bash
# Connect to production database
sqlcmd -S production-server -d UniManage -i backend/database/migrations/XX_MigrationName.sql

# Verify migration
sqlcmd -S production-server -d UniManage -Q "SELECT * FROM table_name"
```

### 4. Deploy Backend

**Build backend:**

```bash
cd backend/src/UniManage.Api

# Restore packages
dotnet restore

# Build in Release mode
dotnet build -c Release

# Publish
dotnet publish -c Release -o ./publish
```

**Deploy to server:**

```bash
# Copy to production server
scp -r ./publish/* user@production-server:/var/www/unimanage/backend/

# Or use Docker
docker build -t unimanage-backend:latest .
docker push your-registry/unimanage-backend:latest

# On production server
docker pull your-registry/unimanage-backend:latest
docker stop unimanage-backend
docker rm unimanage-backend
docker run -d --name unimanage-backend -p 5297:5297 your-registry/unimanage-backend:latest
```

**Restart backend service:**

```bash
ssh production-server

# Systemd service
sudo systemctl restart unimanage-backend
sudo systemctl status unimanage-backend

# Or Docker
docker restart unimanage-backend

# Check logs
sudo journalctl -u unimanage-backend -n 50 -f
# Or
docker logs -f unimanage-backend
```

### 5. Deploy Frontend

**Build frontend:**

```bash
cd frontend/uni-manage

# Install dependencies (if needed)
npm install

# Build for production
npm run build

# Test production build locally
npm run start
```

**Deploy to server:**

```bash
# Copy build to production server
scp -r .next/* user@production-server:/var/www/unimanage/frontend/.next/
scp -r public/* user@production-server:/var/www/unimanage/frontend/public/

# Or use Docker
docker build -t unimanage-frontend:latest .
docker push your-registry/unimanage-frontend:latest

# On production server
docker pull your-registry/unimanage-frontend:latest
docker stop unimanage-frontend
docker rm unimanage-frontend
docker run -d --name unimanage-frontend -p 3000:3000 your-registry/unimanage-frontend:latest
```

**Restart frontend service:**

```bash
ssh production-server

# PM2 (Node.js process manager)
pm2 restart unimanage-frontend
pm2 status

# Or Systemd
sudo systemctl restart unimanage-frontend

# Or Docker
docker restart unimanage-frontend
```

### 6. Verify Deployment

**Backend Health Check:**

```bash
# Check API health endpoint
curl https://api.unimanage.com/health

# Check specific endpoints
curl https://api.unimanage.com/api/v1/users

# Check logs
ssh production-server
tail -f /var/www/unimanage/backend/logs/$(date +%Y-%m-%d)/System.log
```

**Frontend Health Check:**

```bash
# Check website loads
curl https://unimanage.com

# Check in browser
open https://unimanage.com

# Verify:
# - Login works
# - Dashboard loads
# - API calls succeed
# - No console errors
```

**Database Verification:**

```bash
# Check migration applied
sqlcmd -S production-server -d UniManage -Q "
  SELECT TOP 10 * FROM MigrationHistory ORDER BY AppliedAt DESC
"

# Check data integrity
sqlcmd -S production-server -d UniManage -Q "
  SELECT COUNT(*) FROM sy_users;
  SELECT COUNT(*) FROM hr_departments;
"
```

### 7. Monitor for Issues

**First 30 minutes after deployment:**

```bash
# Monitor backend logs
ssh production-server
tail -f /var/www/unimanage/backend/logs/$(date +%Y-%m-%d)/*.log

# Monitor frontend logs
pm2 logs unimanage-frontend --lines 100

# Monitor system resources
htop

# Monitor application metrics
# - Response times
# - Error rates
# - Active users
# - Database connections
```

**Check for errors:**

```bash
# Backend errors
grep -i "error\|exception" /var/www/unimanage/backend/logs/$(date +%Y-%m-%d)/*.log

# Frontend errors (if logging to file)
grep -i "error" /var/www/unimanage/frontend/logs/*.log
```

### 8. Rollback if Needed

**If issues found, rollback immediately:**

**Rollback Database:**

```bash
# Run rollback migration
sqlcmd -S production-server -d UniManage -i backend/database/migrations/XX_MigrationName_Rollback.sql
```

**Rollback Backend:**

```bash
ssh production-server

# Restore from backup
cd /var/www/unimanage/backend
rm -rf *
tar -xzf ~/backups/unimanage_YYYYMMDD_HHMMSS.tar.gz

# Restart service
sudo systemctl restart unimanage-backend

# Or Docker
docker stop unimanage-backend
docker rm unimanage-backend
docker run -d --name unimanage-backend -p 5297:5297 your-registry/unimanage-backend:previous-version
```

**Rollback Frontend:**

```bash
ssh production-server

# Restore from backup
cd /var/www/unimanage/frontend
rm -rf .next
tar -xzf ~/backups/frontend_YYYYMMDD_HHMMSS.tar.gz

# Restart service
pm2 restart unimanage-frontend

# Or Docker
docker stop unimanage-frontend
docker rm unimanage-frontend
docker run -d --name unimanage-frontend -p 3000:3000 your-registry/unimanage-frontend:previous-version
```

### 9. Post-Deployment Tasks

**Notify team:**

```bash
# Send deployment notification
# - Slack/Teams message
# - Email to stakeholders
# - Update deployment log
```

**Update documentation:**

-   Update version number in README
-   Add entry to CHANGELOG.md
-   Update deployment documentation if process changed

**Tag release in git:**

```bash
git tag -a v1.2.3 -m "Release version 1.2.3"
git push origin v1.2.3
```

**Clean up:**

```bash
# Remove old backups (keep last 7 days)
find ~/backups -name "unimanage_*" -mtime +7 -delete

# Clean up old Docker images
docker image prune -a -f
```

### 10. Verify Checklist

✅ Pre-deployment checklist completed
✅ Database backed up
✅ Application backed up
✅ Database migrations applied successfully
✅ Backend deployed and running
✅ Frontend deployed and running
✅ Health checks passing
✅ No errors in logs
✅ Login functionality works
✅ Core features tested
✅ Performance acceptable
✅ Team notified
✅ Documentation updated
✅ Git tagged

## Deployment Schedule

**Recommended deployment windows:**

-   **Production**: Tuesday/Wednesday 2-4 AM (lowest traffic)
-   **Staging**: Any weekday 9 AM - 5 PM
-   **Hotfixes**: As needed (follow expedited process)

**Avoid deploying:**

-   Friday afternoons (no support over weekend)
-   During business hours (high traffic)
-   Before major holidays
-   During month-end/year-end processing

## Emergency Hotfix Process

For critical bugs in production:

1. **Create hotfix branch from main**
2. **Make minimal fix (only what's needed)**
3. **Test thoroughly in staging**
4. **Deploy to production immediately**
5. **Monitor closely for 1 hour**
6. **Merge hotfix back to main**

```bash
# Create hotfix
git checkout main
git checkout -b hotfix/critical-bug-fix

# Make fix
# ... code changes ...

# Test and deploy (skip normal schedule)
# ... follow deployment steps ...

# Merge back
git checkout main
git merge hotfix/critical-bug-fix
git push origin main
```

## Summary

This workflow ensures safe, reliable deployments with:

-   Comprehensive pre-deployment checks
-   Database and application backups
-   Proper migration execution
-   Service deployment and restart
-   Health verification
-   Monitoring and rollback procedures
-   Post-deployment tasks
