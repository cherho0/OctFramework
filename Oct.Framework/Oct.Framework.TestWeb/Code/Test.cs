using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oct.Framework.TestWeb.Code
{
    public class Point
    {
        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }
        public float X;
        public float Y;
    }

    public class Test
    {
        //坐标 
        public static double ax, ay, bx, by, cx, cy, zx, zy;
        //距离 
        public static double la, lb, lc;
        //public static double cx1, cy1, cx2, cy2, cx3, cy3, cx4, cy4;
        public static Point Get()
        {
            int i = 0, j = 0, n = 0;
            int okflag = 0;
            int err = 0;
            int aberr = 0;
            int acerr = 0;
            int abcerr = 0;
            int len;
            int abrequery = 0;
            int acrequery = 0;
            int requery1 = 0;
            int loopnum = 1;
            int option = 0;
            double cax, cay, cbx, cby, ccx, ccy;
            double[] cax_abeyance1 = new double[6000000];
            double[] cay_abeyance1 = new double[6000000];
            double[] cbx_abeyance1 = new double[6000000];
            double[] cby_abeyance1 = new double[6000000];
            double[] ccx_abeyance1 = new double[6000000];
            double[] ccy_abeyance1 = new double[6000000];
            double[] cabx_abeyance1 = new double[6000000];
            double[] caby_abeyance1 = new double[6000000];
            double[] cacx_abeyance1 = new double[6000000];
            double[] cacy_abeyance1 = new double[6000000];
            //遍历圆周上所有点 
            // printf("use 'Enter' to divide x y\n"); 
            // printf("input A coordinate:\n"); 
            //  scanf("%lf%lf",&ax,&ay); 
            if ((ax > 1000) || (bx > 1000))
            {
                //   printf("figure is too big! input A coordinate again:\n"); 
                //    scanf("%lf%lf",&ax,&ay); 
            }
            // printf("input B coordinate:\n"); 
            // scanf("%lf%lf",&bx,&by); 
            if ((bx > 1000) || (bx > 1000))
            {
                //   printf("figure is too big! input B coordinate again:\n"); 
                //   scanf("%lf,%lf",&bx,&by);   
            }
            //  printf("input C coordinate:\n"); 
            //   scanf("%lf%lf",&cx,&cy); 
            if ((cx > 1000) || (cx > 1000))
            {
                //    printf("figure is too big! input C coordinate again:\n"); 
                //    scanf("%lf%lf",&cx,&cy); 
            }
            //  printf("input distance from A to Z:\n"); 
            //  scanf("%lf",&la); 
            if (la > 1000)
            {
                //   printf("figure is too big! inputdistance from A to Z again:\n"); 
                //   scanf("%lf",&la); 
            }
            //  printf("input distance from B to Z:\n"); 
            //  scanf("%lf",&lb); 
            if (lb > 1000)
            {
                //  printf("figure is too big! inputdistance from B to Z again:\n"); 
                //    scanf("%lf",&lb); 
            }
            //   printf("input distance from C to Z:\n"); 
            // scanf("%lf",&lc); 
            if (lc > 1000)
            {
                //    printf("figure is too big! inputdistance from C to Z again:\n"); 
                //    scanf("%lf",&lc); 
            }
            /*  printf("\n"); 
              printf("A(%lf,%lf)\n",ax,ay); 
              printf("B(%lf,%lf)\n",bx,by); 
              printf("C(%lf,%lf)\n",cx,cy); 
              printf("AZ = %lf\n",la); 
              printf("BZ = %lf\n",lb); 
              printf("CZ = %lf\n",lc); */
            //INPUT: 
            for (zx = ax - la; zx <= ax + la; zx++)
            {
                do
                {
                    for (zy = ay - la; zy <= ay + la; zy++)
                    {
                        cax = zx - ax;
                        cax = Math.Abs(cax);
                        cax = Math.Pow(cax, 2);
                        cay = zy - ay;
                        cay = Math.Abs(cay);
                        cay = Math.Pow(cay, 2);
                        if (((cax + cay) > (la * la - err)) && ((cax + cay) < (la * la + err)))
                        {
                            okflag = 1;
                            cax_abeyance1[i] = zx;
                            cay_abeyance1[i] = zy;
                            n++;
                        }
                    }
                    if (okflag != 1) err++;
                } while (okflag == 0);
                i++;
                err = 0;
                okflag = 0;
            }
            // printf("query...\n"); 
            i = 0;
            for (zx = bx - lb; zx <= bx + lb; zx++)
            {
                do
                {
                    for (zy = by - lb; zy <= by + lb; zy++)
                    {
                        cbx = zx - bx;
                        cbx = Math.Abs(cbx);
                        cbx = Math.Pow(cbx, 2);
                        cby = zy - by;
                        cby = Math.Abs(cby);
                        cby = Math.Pow(cby, 2);
                        if (((cbx + cby) > (lb * lb - err)) && ((cbx + cby) < (lb * lb + err)))
                        {
                            okflag = 1;
                            cbx_abeyance1[i] = zx;
                            cby_abeyance1[i] = zy;
                        }
                    }
                    if (okflag != 1) err++;
                } while (okflag == 0);
                i++;
                err = 0;
                okflag = 0;
            }
            // printf("query...\n"); 
            //getchar();
            i = 0;
            for (zx = cx - lc; zx <= cx + lc; zx++)
            {
                do
                {
                    for (zy = cy - lc; zy <= cy + lc; zy++)
                    {
                        ccx = zx - cx;
                        ccx = Math.Abs(ccx);
                        ccx = Math.Pow(ccx, 2);
                        ccy = zy - cy;
                        ccy = Math.Abs(ccy);
                        ccy = Math.Pow(ccy, 2);
                        if (((ccx + ccy) > (lc * lc - err)) && ((ccx + ccy) < (lc * lc + err)))
                        {
                            okflag = 1;

                            ccx_abeyance1[i] = zx;
                            ccy_abeyance1[i] = zy;
                            n++;
                        }
                    }
                    if (okflag != 1) err++;
                } while (okflag == 0);
                i++;
                err = 0;
                okflag = 0;
            }
            //printf("query a b c over!\n");
            //  printf("press Enter to continue!\n");
            //必须加不然运行出错，但不会停止 
            // getchar();
            do
            {
                //找ab 交点，因为距离的偏差，所以坐标会有偏差 
                aberr = 0;
                do
                {
                    for (i = 0; i <= 2 * la; i++)
                    {
                        for (j = 0; j <= 2 * lb; j++)
                        {
                            if (cax_abeyance1[i] == cbx_abeyance1[j])
                            {

                                if ((cay_abeyance1[i] > (cby_abeyance1[j] - aberr - abcerr)) && (cay_abeyance1[i] < (cby_abeyance1[j] + aberr + abcerr)))
                                {
                                    cabx_abeyance1[abrequery] = cax_abeyance1[i];
                                    caby_abeyance1[abrequery] = cay_abeyance1[i];
                                    //printf("cabx_abeyance1[%d]=%f", abrequery, cabx_abeyance1[abrequery]);
                                    // printf("caby_abeyance1[%d]=%f\n\n", abrequery, caby_abeyance1[abrequery]);
                                    abrequery++;
                                }
                            }
                        }
                    }
                    if (abrequery < loopnum) aberr++;
                } while (abrequery < loopnum);
                acerr = 0;
                //找ac 交点 
                do
                {
                    for (i = 0; i <= 2 * la; i++)
                    {
                        for (j = 0; j <= 2 * lc; j++)
                        {
                            if (cax_abeyance1[i] == ccx_abeyance1[j])
                            {
                                if ((cay_abeyance1[i] > (ccy_abeyance1[j] - acerr - abcerr)) && (cay_abeyance1[i] < (ccy_abeyance1[j] + acerr + abcerr)))
                                {
                                    cacx_abeyance1[acrequery] = cax_abeyance1[i];
                                    cacy_abeyance1[acrequery] = cay_abeyance1[i];
                                    acrequery++;
                                }
                            }
                        }
                    }
                    if (acrequery < loopnum) acerr++;
                } while (acrequery < loopnum);
                //找交点的交点 
                for (i = 0; i < abrequery; i++)
                {
                    for (j = 0; j < acrequery; j++)
                    {
                        if (cabx_abeyance1[i] == cacx_abeyance1[j])
                        {
                            requery1 = 1;
                            var p = new Point((float)cabx_abeyance1[i], (float)caby_abeyance1[i]);
                            return p;
                        }
                    }
                }
                if (requery1 == 0)
                {
                    loopnum++;
                    abcerr++;
                }
                if (loopnum == 10)
                {
                    // printf("chech input figure is right\n");
                }
            } while (requery1 == 0);
            //printf("finish!\n");
            // getchar();
            return new Point(0, 0);
        }

    }
}